
using Application.Entities;

namespace Application.Interface.Repositories;

public interface ICategoryRepository {
    public Task<List<Category>> GetCategorys();
    
    public Task<Category?> GetCategory(int id);

    public Task DeleteCategory(Category c);

    public Task<Category> CreateCategory(Category c);

    public Task UpdateCategory(Category c);
}