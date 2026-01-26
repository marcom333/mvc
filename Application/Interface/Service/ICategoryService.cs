using Application.Entities;

namespace Application.Interface.Service;

public interface ICategoryService
{
    
    public Category GetCategory(int id);

    public List<Category> GetCategories();

    public bool CreateCategory(Category category);

    public bool UpdateCategory(Category category);
}