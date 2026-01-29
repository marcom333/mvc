using Application.Entities;

namespace Application.Interface.Services;

public interface IProductService
{
    public Product? GetProduct(int id);
    public Task<List<Product>> GetProducts();
    public Product CreateProduct(Product product);
    public void UpdateProduct(Product product);
    public void DeleteProduct(Product product);
}