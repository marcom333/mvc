using System.Data;
using Application.Entities;
using Infrastructure.Data;
using Dapper;
using Application.Interface.Service;

namespace Infrastructure.Repositories;

public class ProductRepository:IProductService
{
    private readonly IDapperService _context;
    public ProductRepository(IDapperService context)
    {
        _context = context;
    }

    public async Task<List<Product>>GetProducts()
    {
        using IDbConnection con = _context.GetConnection();
        con.Open();
        string query = @"SELECT 
                            ProductId,
                            CategoryId,
                            UserId,
                            Price,
                            Name,
                            Description
                        FROM
                            dbo.Product";
        return (await con.QueryAsync<Product>(query)).ToList();
    }

    public async Task<Product> GetProduct(int id) {
        using IDbConnection con = _context.GetConnection();
        con.Open();
        string sql =
            @"SELECT
                PROD.ProductId AS ProductId,
                PROD.NAME AS Name,
                PROD.PRICE AS Price,
                PROD.DESCRIPTION AS Description,
                PROD.UserId AS UserId,
                PROD.CategoryId AS CategoryId,
                CAT.Name,
                CAT.Description,
                CAT.ParentCategoryId
            FROM dbo.PRODUCT PROD
            JOIN dbo.Categories CAT ON PROD.CategoryId = CAT.CategoryId
            WHERE PRODUCT_ID = @id";
        return (await con.QueryAsync<Product,Category,Product>(sql,(product,category) =>
        {
            product.Category = category;
            return product;
        },
        splitOn:"CategoryId",param:new{id})).First();
    }
    public async Task<bool> DeleteProduct(Product p) {
        using IDbConnection con = _context.GetConnection();
        
        con.Open();
        string sql = @"DELETE FROM dbo.PRODUCT WHERE PRODUCT_ID = @ProductId";
        return await con.ExecuteAsync(sql,p)>0;
    }
    public async Task<int> CreateProduct(Product p) {
        using IDbConnection con = _context.GetConnection();
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
        return await con.ExecuteScalarAsync<int>(sql,p);
    }
    public async Task<bool> UpdateProduct(Product p) {
        IDbConnection con = _context.GetConnection();
        con.Open();
        string sql =
            @"UPDATE dbo.PRODUCT SET
                NAME = @Name,
                PRICE = @Price,
                DESCRIPTION = @Description,
                USER_ID =@UserId,
                CATEGORY_ID = @CategoryId
            WHERE PRODUCT_ID = @ProductId";
        return await con.ExecuteAsync(sql,p)>0;
    }
}