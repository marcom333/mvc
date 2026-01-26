using Application.Entities;

namespace Application.Interface.Service;

public interface IProductService
{
    public Product? GetProduct(int ind);
    public List<Product>GetProducts();
    public void CreateProduct(Product product);
    public void UpdateProduct(Product product);
    public void DeleteProduct(Product product);
}