

using Application.Entities;

namespace Application.Interface.Service;

public interface IProductService
{

    public Task <Product?> GetProduct(int ind);
    public Task<List<Product>> GetProducts();
    public Task<Product> CreateProduct(Product p);
    public Task  UpdateProduct(Product p);
    public Task DeleteProduct(Product p);

}