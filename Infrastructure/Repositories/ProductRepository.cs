

using System.Data;
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


}
