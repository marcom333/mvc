using System.Threading.Tasks;
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

    public async Task<Product> GetProduct(int id)
    {
        return await _productRepository.GetProduct(id);
    }

    public async Task<List<Product>> GetProducts()
    {
        return await _productRepository.GetProducts();
    }
    
    public async Task<bool> CreateProduct(Product product)
    {
        try
        {
            return await _productRepository.CreateProduct(product);
        }catch(Exception ex)
        {
            Console.WriteLine($"Ocurrio un error al guardar el producto: {ex}");
            return false;
        }
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        try
        {
            return await _productRepository.UpdateProduct(product);
        }catch(Exception ex)
        {
            Console.WriteLine($"Ocurrio un error al actualizar el producto: {ex}");
            return false;
        }
    }
    
    public async Task<bool> DeleteProduct(int id)
    {
        try
        {
            return await _productRepository.DeleteProduct(id);
        }catch(Exception ex)
        {
            Console.WriteLine($"Ocurrio un error al eliminar el producto: {ex}");
            return false;
        }
    }
}