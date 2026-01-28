using Application.Interface.Service;
using Application.Entities;

namespace Application.Services;

public class ProductService
{
    private List<Product>Products;
    public void CreateProduct(Product product)
    {
        Products.Add(product);
    }

    public Product? GetProduct(int ind)
    {
        foreach(Product product in Products)
        {
            if(product.ProductId == ind)
            {
                return product;
            }
        }
        return null;
    }

    public List<Product> GetProducts()
    {
        return Products;
    }

    public void UpdateProduct(Product updated_product)
    {
        for(int i = 0;i<Products.Count();i++)
        {
            if(Products[i].ProductId == updated_product.ProductId)
            {
                Products[i] = updated_product;
            }
        }
    }

    public void DeleteProduct(Product product)
    {
        for(int i = 0;i<Products.Count();i++)
        {
            if(Products[i].ProductId == product.ProductId)
            {
                Products.RemoveAt(i);
            }
        }
    }

    public ProductService()
    {
        Products = [
            new Product()
            {
                ProductId = 1,
                Name = "Billete de $50",
                Price = 100,
                Description = "Un billete morado con un ajolotito bien bonito",
                UserId = 1,
                CategoryId = 1
            },
            new Product()
            {
                ProductId = 2,
                Name = "Billete de $100",
                Price = 200,
                Description = "Un billete medio rojillo/naranja usualmente bien fregado",
                UserId = 1,
                CategoryId = 2
            },
            new Product()
            {
                ProductId = 3,
                Name = "Billete de $20",
                Price = 40,
                Description = "Benito Juárez",
                UserId = 1,
                CategoryId = 3
            }
        ];
    }


}