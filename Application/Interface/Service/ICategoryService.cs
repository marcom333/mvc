using Application.Entities;
namespace Application.Interface.Service;

public interface ICategoryService
{
    public Task<Category> CreateCategory(Category category);
    public Task<Category?> GetCategory(int id);
    public Task<List<Category>> GetCategories();
    public Task UpdateCategory(Category category);
    public Task DeleteCategory(Category category);
}
    