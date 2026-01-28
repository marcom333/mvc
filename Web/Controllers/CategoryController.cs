using Application.Entities;
using Application.Interface.Service;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class CategoryController : Controller {

    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService productService) {
        _categoryService = productService;
    }
    
    public async Task<IActionResult> Index() {
        if(TempData["error"] != null)
            ViewBag.Error = "No existen más productos de esa categoría";
        return View(await _categoryService.GetCategorys());
    }
    
    public async Task<IActionResult> Detail(int id) {
        Category? p = await _categoryService.GetCategory(id);
        if(p == null) return NotFound();
        return View(p);
    }
    
    public IActionResult Create() {
        return View(new Category());
    }

    [HttpPost]
    public async Task<IActionResult> Create(Category p) {
        if(p.Name == "") return BadRequest();
        if(p.Description == "") return BadRequest();
        TempData["status"] = 200;
        Category product = await _categoryService.CreateCategory(p);

        return RedirectToAction("Detail", new {id=product.CategoryId});
    }
    
    public async Task<IActionResult> Update(int id) {
        Category? p = await _categoryService.GetCategory(id);
        if(p == null) return NotFound();
        return View(p);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, Category p) {
        p.CategoryId = id;
        if(p.Name == "") return BadRequest();
        if(p.Description == "") return BadRequest();
        await _categoryService.UpdateCategory(p);

        return RedirectToAction("Detail", "Category", new {id, name="hola", registrado=true});
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id) {
        Category? p = await _categoryService.GetCategory(id);
        if(p == null) return NotFound();
        await _categoryService.DeleteCategory(p);
        return RedirectToAction("Index");
    }
}