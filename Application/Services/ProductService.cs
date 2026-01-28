using System.Threading.Tasks;
using Application.Entities;
using Application.Interface.Repositories;
using Application.Interface.Service;

namespace Application.Services;

public class ProductService : IProductService
{    
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> GetProduct(int id)
    {
        try
        {
            Product? product = await _productRepository.GetProduct(id);
            return product != null? product: new Product();
        }catch(Exception ex)
        {
            Console.WriteLine($"Ocurrio un error al guardar el producto: {ex}");
            return new Product();
        }
    }

    public async Task<List<Product>> GetProducts()
    {
        try
        {
            return await _productRepository.GetProducts();
        }catch(Exception ex)
        {
            Console.WriteLine($"Ocurrio un error al obtener los productos: {ex}");
            return new List<Product>();
        }
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