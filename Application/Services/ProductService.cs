
using Application.Entities;
using Application.Interface.Service;

namespace Application.Services;

public class ProductService : IProductService {

    private List<Product> Products;
    private int Index = 3;

    public ProductService() {
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

    public Product CreateProduct(Product product) {
        product.ProductId = ++Index;
        Products.Add(product);
        return product;
    }

    public Product? GetProduct(int id) {
        foreach(Product p in Products) {
            if(p.ProductId == id)
                return p;
        }
        return null;
    }

    public List<Product> GetProducts() {
        return Products;
    }

    public void UpdateProduct(Product product) {
        foreach(Product p in Products) {
            if(p.ProductId == product.ProductId) {
                p.Description = product.Description;
                p.Name = product.Name;
                p.Price = product.Price;
                p.CategoryId = product.CategoryId;
                p.UserId = product.UserId;
            }
        }
    }

    public void DeleteProduct(Product product) {
        for(int i = 0; i < Products.Count; i++) {
            if(Products[i].ProductId == product.ProductId) {
                Products.Remove(Products[i]);
            }
        }
    }
}
