
using System.Reflection;
using Application.Entities;
using Application.Interface.Service;

namespace Application.Services;

public class ProductService : IProductService {

    private List<Product> Products;

    public ProductService() {
        Products = [
            new Product(){
                Name = "Lechuga",
                Price = 5,
                Description = "Lechuga normal",
                UserId = 1,
                CategoryId = 1,
            },
            new Product(){
                Name = "Tomate",
                Price = 5,
                Description = "Tomate normal",
                UserId = 1,
                CategoryId = 1,
            },
            new Product(){
                Name = "Manzana",
                Price = 5,
                Description = "Manzana normal",
                UserId = 1,
                CategoryId = 2,
            }
        ];
    }

    public Product CreateProduct(Product product) {
        throw new NotImplementedException();
    }

    public Product GetProduct(int ind) {
        throw new NotImplementedException();
    }

    public List<Product> GetProducts() {
        return Products;
    }

    public void UpdateProduct(Product product) {
        throw new NotImplementedException();
    }
}
