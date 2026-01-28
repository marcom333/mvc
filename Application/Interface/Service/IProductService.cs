

using Application.Entities;

namespace Application.Interface.Service;

public interface IProductService {
    
    public Task<Product?> GetProduct(int ind);
    public Task<List<Product>> GetProducts();
    public Task<Product> CreateProduct(Product product);
    public Task UpdateProduct(Product product);
    public Task DeleteProduct(Product product);

}