using Application.Entities;

namespace Application.Interface.Repositories;

public interface ICategoryRepository
{
    public Task<Category> GetCategory(int id);

    public Task<List<Category>> GetCategories();

    public Task<bool> CreateCategory(Category category);
    
    public Task<bool> UpdateCategory(Category category);
    
    public Task<bool> DeleteCategory(int id);
}