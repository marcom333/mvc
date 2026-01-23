using Application.Entities;
using Application.Interface.Service;

namespace Application.Services;

public class ProductService : IProductService
{
    private List<Product> Products;

    public ProductService()
    {
        Products = new List<Product>()
        {
            new Product()
            {
                Name = "KeyBoard",
                Price = 250,
                Description = "Redragon Keyboard",
                UserId = 1,
                CategoryId = 1,
            },            
            new Product()
            {
                Name = "Mouse",
                Price = 150,
                Description = "Vorago Mouse",
                UserId = 1,
                CategoryId = 1,
            },
            new Product()
            {
                Name = "MotherBoard GTX",
                Price = 2050,
                Description = "Motherboard GTX Gaming Xperience",
                UserId = 1,
                CategoryId = 2,
            }
        };    
    }

    public Product CreateProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public Product GetProduct(int id)
    {
        throw new NotImplementedException();
    }

    public List<Product> GetProducts()
    {
        return this.Products;
    }

    public bool UpdateProduct(Product product)
    {
        throw new NotImplementedException();
    }
}