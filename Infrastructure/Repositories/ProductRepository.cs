

using System.Data;
using System.Globalization;
using Application.Entities;
using Application.Interface.Repositories;
using Dapper;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{


    private readonly DapperContext _context;

    public ProductRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetProducts()
    {
        using IDbConnection con = _context.GetConnection();
        con.Open();
        return (await con.QueryAsync<Product>(@"
            SELECT 
                ProductId, 
                CategoryId, 
                UserId, 
                Price, 
                Name, 
                Description
            FROM dbo.Product;
        ")).ToList();
    }

    public async Task<Product?> GetProduct(int id)
    {
        using IDbConnection conn = _context.GetConnection();
        conn.Open();
        string sql =
            @"SELECT 
                ProductId AS Product_id, 
                Name AS Name, 
                Price AS Price, 
                Description AS Description, 
                UserId AS UserId, 
                CategoryId AS CategoryId 
            FROM dbo.PRODUCT 
            WHERE ProductId = @productId";
        return await conn.QuerySingleOrDefaultAsync<Product>(sql, new { productId = id});
        
    }
    public async Task DeleteProduct(Product p)
    {
        using IDbConnection conn = _context.GetConnection();
        conn.Open();
        string sql = @"DELETE FROM dbo.Product WHERE ProdcutId = @ProductId";
        int rows = await conn.ExecuteAsync(sql,p);
    }
    public async Task<Product> CreateProduct(Product p)
    {
        using IDbConnection con = _context.GetConnection();
        con.Open();
        string sql =
            @"INSERT INTO dbo.PRODUCT (
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
    public async Task UpdateProduct(Product p)
    {
        using IDbConnection con = _context.GetConnection();
        con.Open();
        string sql =
            @"UPDATE dbo.PRODUCT SET
                NAME = @Name, 
                PRICE = @Price, 
                DESCRIPTION = @Description, 
                UserId =@UserId, 
                CategoryId = @CategoryId
            WHERE ProductId = @ProductId";
        await con.ExecuteAsync(sql, p);
    }
}
