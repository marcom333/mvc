using Application.Entities;

namespace Application.Interface.Repositories;

public interface ICategoryRepository{
    public Task<Category> CreateCategory(Category p);
    public Task<Category?> GetCategory(int id);
    public Task<List<Category>> GetCategories();
    public Task UpdateCategory(Category p);
    public Task DeleteCategory(Category p);
}