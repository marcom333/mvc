using Application.Entities;
using Application.Interface.Service;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    public IActionResult Index()
    {
        ViewData["nav"] = "category";
        return View(_categoryService.GetCategories());
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        ViewData["nav"] = "category";
        return View();
    }
    
    [HttpPost]
    public IActionResult Store(Category category)    
    {
        ViewData["nav"] = "category";
        if (!ModelState.IsValid)
        {
            TempData["error"] = "La categoría no fue registrada!";
            return View("Create", category);
        }

        if (_categoryService.CreateCategory(category))
        {
            TempData["success"] = "La categoría fue registrada Exitosamente!";
            return RedirectToAction(nameof(Index));
        }
        else
        {            
            TempData["error"] = "Ocurrio un error. La categoría no fue registrada!";
            return RedirectToAction(nameof(Index));
        }
    }
    
    [HttpGet]
    public IActionResult Details()
    {
        ViewData["nav"] = "category";

        Category category = new Category()
        {
          CategoryId = 1,
          Name = "Electrónica",
          Description = "En globa todos los productos de electrónica"
        };

        return View(category);
    }
    
    [HttpGet("Category/Edit/{id:int}")]
    public IActionResult Edit(int id)
    {    
        ViewData["nav"] = "category";
        if (id is 0)
        {
            TempData["error"] = "La categoría no fue encontrada!";
            return RedirectToAction(nameof(Index));
        }

        return View(_categoryService.GetCategory(id));        
    }

    [HttpPost]
    public IActionResult Update(Category category)
    {
        ViewData["nav"] = "category";
        if (!ModelState.IsValid)
        {
            TempData["error"] = "La categoría no fue actualizada!";
            return View("Edit", category);
        }
        
        if (_categoryService.UpdateCategory(category))
        {
            TempData["success"] = "La categoría fue actualizada Exitosamente!";
            return RedirectToAction(nameof(Index));
        }
        else
        {            
            TempData["error"] = "Ocurrio un error. La categoría no fue actualizada!";
            return RedirectToAction(nameof(Index));
        }
    }
}