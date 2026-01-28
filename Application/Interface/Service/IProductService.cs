using Application.Entities;

namespace Application.Interface.Service;

public interface IProductService
{
    public Task<Product> GetProduct(int id);
    public Task<List<Product>>GetProducts();
    
    public Task<int> CreateProduct(Product p);
    public Task<bool> UpdateProduct(Product p);
    public Task<bool> DeleteProduct(Product p);
}