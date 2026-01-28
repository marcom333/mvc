using Application.Entities;

namespace Web.ViewModels;

public class ProductViewModel: Product
{
    public List<Category>? categories = new List<Category>();

    public List<User>? users = new List<User>();
}