using Application.Entities;
using Application.Interface.Service;

namespace Application.Services;

public class CategoryService : ICategoryService
{
    
    private List<Category> Categories;

    public CategoryService()
    {
        Categories = new List<Category>()
        {
            new Category()
            {
                Name = "Accesorios",
                Description = "En globa productos de la categoria accesorios."
            },
            new Category()
            {
                Name = "Perifericos",
                Description = "En globa productos de la categoria Perifericos."
            },
            new Category()
            {
                Name = "Computación",
                Description = "En globa productos de la categoria computación."
            },
        };    
    }

    public Task<Category> GetCategory(int id)
    {
        //dapper where id = id
        Category category = new Category()
        {
            Name = "Electrodomésticos",
            Description = "En globa todos los productos de la categoría electrodomésticos",
        };

        return category;
    }

    public Task<List<Category>> GetCategories()
    {
        return this.Categories;
    }
    
    public Task<bool> CreateCategory(Category category)
    {
        //dapper Saving
        Category newCategory = category;
        return newCategory != null;
    }

    public Task<bool> UpdateCategory(Category category)
    {
        //dapper Updating
        Category Category = category;
        return false;
    }
    
    public Task<bool> DeleteCategory(Category category)
    {
        //dapper Updating
        Category Category = category;
        return false;
    }
}