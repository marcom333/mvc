using Application.Interface.Service;
using Application.Entities;

namespace Application.Services;

public class CategoryService : ICategoryService
{
    private List<Category>Categories;
    public CategoryService()
    {
        Categories = [
            new Category(){
                CategoryId = 1,
                Name = "Nuevos",
                ParentCategoryID = null
            },
            new Category(){
                CategoryId = 2,
                Name="Usados",
                ParentCategoryID = null
            },
            new Category(){
                CategoryId = 3,
                Name = "Rotos",
                ParentCategoryID = 2
            }
        ];

    }
    
    public Category? GetCategory(int id)
    {
        foreach(Category category in Categories)
        {
            if(category.CategoryId == id)
            {
                return category;
            }
        }
        return null;
    }

    public List<Category> GetCategories()
    {
        return Categories;
    }

    public void CreateCategory(Category category)
    {
        Categories.Add(category);
    }

    public void UpdateCategory(Category category)
    {
        for(int i = 0; i < Categories.Count(); i++)
        {
            if(Categories[i].CategoryId == category.CategoryId)
            {
                Categories[i] = category;
            }
        }
    }
}