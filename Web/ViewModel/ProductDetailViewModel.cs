
using Application.Entities;

namespace Web.ViewModel;

public class ProductDetailViewModel
{
    public Product Product { get; set; } = new();
    public Category Category { get; set; } = new();
    public User User { get; set; } = new();
}
