

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

    public async Task<Product> GetProduct(int id) {
        string sql =
            @"SELECT 
                PRODUCT_ID AS Product_id, 
                NAME AS Name, 
                PRICE AS Price, 
                DESCRIPTION AS Description, 
                USER_ID AS UserId, 
                CATEGORY_ID AS CategoryId 
            FROM dbo.PRODUCT 
            WHERE PRODUCT_ID = @id";
        throw new NotImplementedException("Not done");
    }
    public async Task DeleteProduct(Product p) {
        string sql = @"DELETE FROM dbo.PRODUCT WHERE PRODUCT_ID = @Product_id";
        throw new NotImplementedException("Not done");
    }
    public async Task<Product> CreateProduct(Product p) {
        string sql = 
            @"INSERT INTO dbo.PRODUCT (
                NAME,
                PRICE,
                DESCRIPTION,
                USER_ID,
                CATEGORY_ID
            )
            VALUES (
                @Name, 
                @Price, 
                @Description, 
                @UserId, 
                @CategoryId
            );
            SELECT CAST(SCOPE_IDENTITY() AS int);";
        throw new NotImplementedException("Not done");
    }
    public async Task UpdateProduct(Product p) {
        string sql = 
            @"UPDATE dbo.PRODUCT SET
                NAME = @Name, 
                PRICE = @Price, 
                DESCRIPTION = @Description, 
                USER_ID =@UserId, 
                CATEGORY_ID = @CategoryId
            WHERE PRODUCT_ID = @Product_id";
        throw new NotImplementedException("Not done");
    }

}