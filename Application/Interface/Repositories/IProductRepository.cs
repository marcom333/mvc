
using Application.Entities;

namespace Application.Interface.Repositories;

public interface IProductRepository {
    public Task<List<Product>> GetProducts();
    
    public Task<Product?> GetProduct(int id);

    public Task DeleteProduct(Product p);

    public Task<Product> CreateProduct(Product p);

    public Task UpdateProduct(Product p);

    public Task<PageResult<Product>> GetAllWithPage(int page = 1, int pageSize = 10, string? name = null);
}