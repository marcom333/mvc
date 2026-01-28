using Application.Entities;
using Application.Interface.Service;
using Application.Interface.Repositories;
using System.Threading.Tasks;

namespace Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository; 
    }

    public async Task<Category> GetCategory(int id)
    {
        try
        {
            Category? category = await _categoryRepository.GetCategory(id);
            return category != null? category: new Category();
        }catch(Exception ex)
        {
            Console.WriteLine($"Ocurrio un error al guardar la categoria: {ex}");
            return new Category();
        }
    }

    public async Task<List<Category>> GetCategories()
    {
        try
        {
            return await _categoryRepository.GetCategories();
        }catch(Exception ex)
        {
            Console.WriteLine($"Ocurrio un error al obtener las categorias: {ex}");
            return new List<Category>();
        }
    }
    
    public async Task<bool> CreateCategory(Category category)
    {
        try
        {
            return await _categoryRepository.CreateCategory(category);
        }catch(Exception ex)
        {
            Console.WriteLine($"Ocurrio un error al guardar la categoria: {ex}");
            return false;
        }
    }

    public async Task<bool> UpdateCategory(Category category)
    {
        try
        {
            return await _categoryRepository.UpdateCategory(category);
        }catch(Exception ex)
        {
            Console.WriteLine($"Ocurrio un error al actualizar la categoria: {ex}");
            return false;
        }
    }
    
    public async Task<bool> DeleteCategory(int id)
    {
        try
        {
            return await _categoryRepository.DeleteCategory(id);
        }catch(Exception ex)
        {
            Console.WriteLine($"Ocurrio un error al eliminar la categoria: {ex}");
            return false;
        }
    }
}