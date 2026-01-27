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
        Product? product = await _productRepository.GetProduct(id);
        return product != null? product: new Product();
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