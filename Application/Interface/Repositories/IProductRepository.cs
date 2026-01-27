using Application.Entities;

namespace Application.Interface.Repositories;

public interface IProductRepository
{
    public Task<Product> GetProduct(int id);

    public Task<List<Product>> GetProducts();

    public Task<bool> CreateProduct(Product product);
    
    public Task<bool> UpdateProduct(Product product);
    
    public Task<bool> DeleteProduct(int id);
}