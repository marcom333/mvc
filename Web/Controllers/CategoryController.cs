using System.Threading.Tasks;
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
    
    public async Task<IActionResult> Index()
    {
        ViewData["nav"] = "category";
        return View(await _categoryService.GetCategories());
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        ViewData["nav"] = "category";
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Store(Category category)    
    {
        ViewData["nav"] = "category";
        if (!ModelState.IsValid)
        {
            TempData["error"] = "La categoría no fue registrada!";
            return View("Create", category);
        }

        if (await _categoryService.CreateCategory(category))
            TempData["success"] = "La categoría fue registrada Exitosamente!";
        else
            TempData["error"] = "Ocurrio un error. La categoría no fue registrada!";
        
        return RedirectToAction(nameof(Index));
    }
    
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        ViewData["nav"] = "category";

        return View(await _categoryService.GetCategory(id));
    }
    
    [HttpGet("Category/Edit/{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {    
        ViewData["nav"] = "category";
        if (id is 0)
        {
            TempData["error"] = "La categoría no fue encontrada!";
            return RedirectToAction(nameof(Index));
        }

        return View(await _categoryService.GetCategory(id));        
    }

    [HttpPost]
    public async Task<IActionResult> Update(Category category)
    {
        ViewData["nav"] = "category";
        if (!ModelState.IsValid)
        {
            TempData["error"] = "La categoría no fue actualizada!";
            return View("Edit", category);
        }
        
        if (await _categoryService.UpdateCategory(category))
            TempData["success"] = "La categoría fue actualizada Exitosamente!";
        else     
            TempData["error"] = "Ocurrio un error. La categoría no fue actualizada!";
        
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        ViewData["nav"] = "product";        
        if (await _categoryService.DeleteCategory(id))
            TempData["success"] = "La categoría fue eliminada Exitosamente!";
        else 
            TempData["error"] = "Ocurrio un error. La categoría no fue eliminada!";
        
        return RedirectToAction(nameof(Index));
    }
}