using Application.Entities;

namespace Application.Interface.Service;

public interface IProductService
{
    public Product GetProduct(int id);

    public List<Product> GetProducts();

    public Product CreateProduct(Product product);

    public bool UpdateProduct(Product product);
}