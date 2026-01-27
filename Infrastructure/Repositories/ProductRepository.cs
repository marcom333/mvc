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

    public async Task<Product> GetProduct(int id)
    {
        Product? product;
        string sql = @"SELECT ProductId, CategoryId, UserId, Name, Description, Price FROM dbo.Product WHERE ProductId = @ID";

        using (var conn = _dapper.GetConnection())
        {
            conn.Open();

            using (var tx = conn.BeginTransaction())
            {
                try
                {
                    product = await conn.QueryFirstAsync<Product>(sql, new { ID = id }, tx);
                    tx.Commit();
                }
                catch { 
                    tx.Rollback();
                    throw;
                }
            }

            return product;
        }
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
    
    public async Task<bool> UpdateProduct(Product product)
    {
        string sql = @"
            UPDATE dbo.Product SET CategoryId = @CategoryId, UserId = @UserId, Name = @Name, Description = @Description, Price = @Price
            WHERE ProductId = @ProductId";

        int filas = 0;

        using (var conn = _dapper.GetConnection())
        {
            conn.Open();
            
            using (var tx = conn.BeginTransaction())
            {
                try
                {
                    filas = await conn.ExecuteAsync(sql, product, tx);
                    if(filas == 0) {                          
                        throw new InvalidOperationException("No se actualizó el producto. Error UpdateProduct.");    
                    }
                    tx.Commit();
                }
                catch { 
                    tx.Rollback();
                    throw;
                }
            }

            return filas > 0;
        }        
    }
    
    public async Task<bool> DeleteProduct(int id)
    {
        string sql = @"
            DELETE FROM dbo.Product WHERE ProductId = @ID";

        bool eliminado = false;

        using (var conn = _dapper.GetConnection())
        {
            conn.Open();
            
            using (var tx = conn.BeginTransaction())
            {
                try
                {                    
                    eliminado = await conn.ExecuteAsync(sql, new { ID = id }, tx) > 0;
                    tx.Commit();
                }
                catch { 
                    tx.Rollback();
                    throw;
                }
            }

            return eliminado;
        }        
    }
}