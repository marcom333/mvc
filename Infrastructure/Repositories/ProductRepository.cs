using Application.Entities;
using Dapper;
using Infrastructure.Data;
using System.Data;
using Application.Interface.Repositories;

namespace Infrastructure.Repositories
{
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
            return (await con.QueryAsync<Product>(
                @"SELECT ProductId, UserId, Price, Name, Description
                From dbo.Product;")).ToList();         
        }
    }
}
