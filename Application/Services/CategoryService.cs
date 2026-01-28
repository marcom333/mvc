
using System.Threading.Tasks;
using Application.Entities;
using Application.Interface.Repositories;
using Application.Interface.Service;

namespace Application.Services;

public class CategoryService : ICategoryService {
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository) {
        _categoryRepository = categoryRepository;
    }

    public async Task<Category> CreateCategory(Category category) {
        return await _categoryRepository.CreateCategory(category);
    }

    public async Task<Category?> GetCategory(int id) {
        return await _categoryRepository.GetCategory(id);
    }

    public async Task<List<Category>> GetCategorys() {
        return await _categoryRepository.GetCategorys();
    }

    public async Task UpdateCategory(Category category) {
        await _categoryRepository.UpdateCategory(category);
    }

    public async Task DeleteCategory(Category category) {
        await _categoryRepository.DeleteCategory(category);
    }
}
