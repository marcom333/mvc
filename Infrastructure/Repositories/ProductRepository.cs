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
        using IDbConnection conn = _dapper.GetConnection();
        conn.Open();
        return (await conn.QueryAsync<Product>(@"
            SELECT 
                ProductId,
                CategoryId,
                UserId,
                Name,
                Description,
                Price
            FROM dbo.Product"
        )).ToList();
    }

    List<Product> IProductRepository.GetProducts()
    {
        throw new NotImplementedException();
    }
}