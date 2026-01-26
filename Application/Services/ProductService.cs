using Application.Entities;
using Application.Interface.Repositories;
using Application.Interface.Service;

namespace Application.Services;

public class ProductService : IProductService
{    
    private List<Product> Products;

    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
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

    public Product GetProduct(int id)
    {
        //dapper where id = id
        Product product = new Product()
        {
            Name = "KeyBoard",
            Description = "Redragon Keyboard",
            Price = 250,
            CategoryId = 1, 
            UserId = 1
        };

        return product;
    }

    public List<Product> GetProducts()
    {
        return _productRepository.GetProducts();
    }
    
    public bool CreateProduct(Product product)
    {
        //dapper Saving
        Product newProduct = product;
        return newProduct != null;
    }

    public bool UpdateProduct(Product product)
    {
        //dapper Updating
        Product Product = product;
        return Product != null;
    }
}