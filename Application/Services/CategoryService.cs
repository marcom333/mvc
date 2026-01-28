using Application.Entities;
using Application.Interface.Repositories;

namespace Application.Services;

public class CategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService (ICategoryRepository categoryRepository){
        _categoryRepository = categoryRepository;
    }

    public async Task<Category?> GetCategory(int id)
    {
        return await _categoryRepository.GetCategory(id);
    }
}