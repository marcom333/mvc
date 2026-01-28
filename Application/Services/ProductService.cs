using Application.Entities;
using Application.Interface.Repositories;
using Application.Interface.Service;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    public ProductService(IProductRepository productRepository) {
        _productRepository = productRepository;
    }
    public async Task<Product> CreateProduct(Product product)
    {
        return await _productRepository.CreateProduct(product);
    }

    public async Task<Product?> GetProduct(int id) {
        return await _productRepository.GetProduct(id);
    }

    public async Task<List<Product>> GetProducts() {
        return await _productRepository.GetProducts();
    }

    public async Task UpdateProduct(Product product){
        await _productRepository.UpdateProduct(product);
    }

    public async Task DeleteProduct(Product product) {
        await _productRepository.DeleteProduct(product);
    }
}