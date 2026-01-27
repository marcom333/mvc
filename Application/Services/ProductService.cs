using Application.Entities;
using Application.Interface.Repositories;
using Application.Interface.Service;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private List<Product> Products;
    private int Index = 3;
    public ProductService(IProductRepository productRepository) {
        _productRepository = productRepository;
        Products = [
            new Product(){
                ProductId = 1,
                Name = "Lechuga",
                Price = 5,
                Description = "Lechuga normal",
                UserId = 1,
                CategoryId = 1,
            },
            new Product(){
                ProductId = 2,
                Name = "Tomate",
                Price = 5,
                Description = "Tomate normal",
                UserId = 1,
                CategoryId = 1,
            },
            new Product(){
                ProductId = 3,
                Name = "Manzana",
                Price = 5,
                Description = "Manzana normal",
                UserId = 1,
                CategoryId = 2,
            }
        ];
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