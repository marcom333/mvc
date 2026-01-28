

using Application.Entities;

namespace Application.Interface.Service;

public interface ICategoryService {
    
    public Task<Category?> GetCategory(int ind);
    public Task<List<Category>> GetCategorys();
    public Task<Category> CreateCategory(Category model);
    public Task UpdateCategory(Category model);
    public Task DeleteCategory(Category model);

}