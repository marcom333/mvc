using Application.Entities;

namespace Application.Interface.Repositories;

public interface IProductRepository {
    public Task<Product> CreateProduct(Product p);
    public Task<Product?> GetProduct(int id);
    public Task<List<Product>> GetProducts();
    public Task UpdateProduct(Product p);
    public Task DeleteProduct(Product p);
}