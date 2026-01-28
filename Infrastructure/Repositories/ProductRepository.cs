using System.Data;
using Application.Entities;
using Application.Interface.Repositories;
using Dapper;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository{
    private readonly DapperContext _context;
    public ProductRepository(DapperContext context) {
        _context = context;
    }

    public async Task<List<Product>> GetProducts() {
        using IDbConnection con = _context.GetConnection();
        con.Open();
        string query = @"
            SELECT 
                ProductId, 
                CategoryId, 
                UserId, 
                Price, 
                Name, 
                Description
            FROM dbo.Product;";
        return (await con.QueryAsync<Product>(query)).ToList();
    }

    public async Task<Product?> GetProduct(int id) {
        using IDbConnection con = _context.GetConnection();
        con.Open();
        string sql =
            @"SELECT 
                p.ProductId,
                p.Name, 
                p.Price, 
                p.Description, 
                p.UserId, 
                p.CategoryId,
                c.CategoryId as CategoryId,
                c.Name,
                c.Description
            FROM dbo.Product p
            INNER JOIN dbo.Category c ON
                p.CategoryId = c.CategoryId
            WHERE ProductId = @productId";
        return (await con.QueryAsync<Product, Category, Product>(sql, 
            (p, c) => {
                p.Category = c;
                return p;
            }, 
            splitOn: "CategoryId",
            param: new {productId=id}
        )).First();
        // return (await con.QueryAsync<Product>(sql, new {productId=id})).FirstOrDefault();
    }
    public async Task DeleteProduct(Product p) {
        using IDbConnection con = _context.GetConnection();
        con.Open();
        string sql = @"DELETE FROM dbo.Product WHERE ProductId = @ProductId";
        int rows = await con.ExecuteAsync(sql, p);
        Console.WriteLine(rows);
        if(rows < 1)
            throw new NotImplementedException("No se borro nada");
    }
    public async Task<Product> CreateProduct(Product p) {
        using IDbConnection con = _context.GetConnection();
        con.Open();
        string sql = 
            @"INSERT INTO dbo.Product (
                Name,
                Price,
                Description,
                UserId,
                CategoryId
            )
            VALUES (
                @Name, 
                @Price, 
                @Description, 
                @UserId, 
                @CategoryId
            );
            SELECT CAST(SCOPE_IDENTITY() AS int);";
        int id = await con.ExecuteScalarAsync<int>(sql, p);
        p.ProductId = id;
        return p;
    }

    public async Task UpdateProduct(Product p) {
        using IDbConnection con = _context.GetConnection();
        con.Open();
        string sql = 
            @"UPDATE dbo.Product SET
                Name = @Name, 
                Price = @Price, 
                Description = @Description, 
                UserId =@UserId, 
                CategoryId = @CategoryId
            WHERE ProductId = @ProductId";
        int count = await con.ExecuteAsync(sql, p);
        Console.WriteLine(count);
    }
}