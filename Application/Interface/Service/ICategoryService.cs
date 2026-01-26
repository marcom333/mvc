using Application.Entities;

namespace Application.Interface.Service;

public interface ICategoryService
{
    public Category? GetCategory(int categoryid);
    public List<Category>GetCategories();
    public void CreateCategory(Category category);
    public void UpdateCategory(Category category);
}