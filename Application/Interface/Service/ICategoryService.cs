using Application.Entities;

namespace Application.Interface.Service;

public interface ICategoryService
{
    
    public Task<Task<Category>> GetCategory(int id);

    public Task<List<Category>> GetCategories();

    public Task<bool> CreateCategory(Category category);

    public bool UpdateCategory(Category category);
}