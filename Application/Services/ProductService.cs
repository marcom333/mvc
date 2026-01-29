using System.Reflection;
using Application.Entities;
using Application.Interface.Repositories;
using Application.Interface.Services;

namespace Application.Services;

public class ProductService : IProductService
{
    private List<Product> Products;
    private int Index = 3;

    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository )
    {
        _productRepository = productRepository;
        Products =[
            new Product()
            {
                ProductId = 1,
                Name = "Producto 1",
                Description = "Descripcion del producto 1",
                Price = 100,
                CategoryId = 1,
                UserId = 1
            },
            new Product()
            {
                ProductId=2,
                Name = "Producto 2",
                Description = "Descripcion del producto 2",
                Price = 200,
                CategoryId = 2,
                UserId = 2
            },
            new Product()
            {
                ProductId=3,
                Name = "Producto 3",
                Description = "Descripcion del producto 3",
                Price = 300,
                CategoryId = 3,
                UserId = 3
            }
            ];
    }
    public Product CreateProduct(Product product)
    {
        product.ProductId = ++Index;
        Products.Add(product);
        return product;

    }

    public Product? GetProduct(int id)
    {
        foreach(Product p in Products)
        {
            if(p.ProductId == id)
            {
                return p;
            }
            
            
        }
        return null;
    }

    public async Task<List<Product>> GetProducts()
    {
        return await _productRepository.GetProducts();
    }

    public void UpdateProduct(Product product)
    {
        foreach (Product p in Products)
        {
            if (p.ProductId == product.ProductId)
            {
                p.Description = product.Description;
                p.Name = product.Name;
                p.Price = product.Price;
                p.CategoryId = product.CategoryId;
                p.UserId = product.UserId;
                p.ProductId = product.ProductId;
            }
           
        }
    }


    public void DeleteProduct(Product product)
    {
        for (int i = 0; i < Products.Count; i++)
        {
            if (Products[i].ProductId == product.ProductId)
            {
                Products.Remove(Products[i]);
                break;
            }
        }
    }

}