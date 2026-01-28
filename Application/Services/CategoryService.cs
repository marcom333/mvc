using Application.Entities;
using Application.Interface.Repositories;
using Application.Interface.Service;

namespace Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    public CategoryService (ICategoryRepository categoryRepository){
        _categoryRepository = categoryRepository;
    }
    public async Task<Category> CreateCategory(Category Category)
    {
        return await _categoryRepository.CreateCategory(Category);
    }

    public async Task<Category?> GetCategory(int id) {
        return await _categoryRepository.GetCategory(id);
    }

    public async Task<List<Category>> GetCategories() {
        return await _categoryRepository.GetCategories();
    }

    public async Task UpdateCategory(Category Category){
        await _categoryRepository.UpdateCategory(Category);
    }

    public async Task DeleteCategory(Category Category) {
        await _categoryRepository.DeleteCategory(Category);
    }
}