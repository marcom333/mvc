using Application.Entities;

namespace Application.Interface.Service;

public interface IProductService
{
    public Product GetProduct(int id);

    public Task<List<Product>> GetProducts();

    public Task<bool> CreateProduct(Product product);

    public bool UpdateProduct(Product product);
}