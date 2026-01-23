using Application.Entities;

namespace Web.ViewModels;

public class ProductDetailViewModel
{
    public Product Product { get; set; } = new Product();

    public Category Category { get; set; } = new Category();

    public User User { get; set; } = new User();
}