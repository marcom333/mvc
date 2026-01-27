using Application.Entities;
namespace Application.Interface.Service;

public interface IProductService
{
    public Task<Product> CreateProduct(Product product);
    public Task<Product?> GetProduct(int id);
    public Task<List<Product>> GetProducts();
    public Task UpdateProduct(Product product);
    public Task DeleteProduct(Product product);
}