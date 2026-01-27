using Application.Entities;

namespace Application.Interface.Repositories;

public interface IProductRepository
{
    public Task<List<Product>> GetProducts();

    public Task<bool> CreateProduct(Product product);
}