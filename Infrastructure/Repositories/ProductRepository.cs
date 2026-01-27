using System.Data;
using Application.Entities;
using Application.Interface.Repositories;
using Dapper;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DapperContext _dapper;
    public ProductRepository(DapperContext dapper)
    {
        _dapper = dapper;
    }

    public async Task<List<Product>> GetProducts()
    {
        List<Product>? products;
        string sql = @"SELECT ProductId, CategoryId, UserId, Name, Description, Price FROM dbo.Product";

        using (var conn = _dapper.GetConnection())
        {
            conn.Open();

            using (var tx = conn.BeginTransaction())
            {
                try
                {
                    products = (await conn.QueryAsync<Product>(sql, null, tx)).ToList();
                    tx.Commit();
                }
                catch { 
                    tx.Rollback();
                    throw;
                }
            }

            return products;
        }
    }

    public async Task<bool> CreateProduct(Product product)
    {
        string sql = @"INSERT INTO dbo.Product (CategoryId, UserId, Name, Description, Price) 
            Values (@CategoryId, @UserId, @Name, @Description, @Price) SELECT CAST(SCOPE_IDENTITY() AS INT)";

        int id = 0;

        using (var conn = _dapper.GetConnection())
        {
            conn.Open();
            
            using (var tx = conn.BeginTransaction())
            {
                try
                {
                    id = await conn.ExecuteScalarAsync<int>(sql, product, tx);
                    tx.Commit();
                }
                catch { 
                    tx.Rollback();
                    throw;
                }
            }

            return id != 0;
        }        
    }
}