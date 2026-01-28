using System.Threading.Tasks;
using Application.Entities;
using Application.Interface.Service;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;

    private readonly ICategoryService _categoryService;

    private readonly IUserService _userService;

    public ProductController(IProductService productService, ICategoryService categoryService, IUserService userService)
    {
        _productService = productService;
        _categoryService = categoryService;
        _userService = userService;
    }

    public async Task<IActionResult> Index()
    {
        ViewData["nav"] = "product";
        ProductIndexViewModel model = new ProductIndexViewModel();
        model.Products = await _productService.GetProducts();
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewData["nav"] = "product";
        ProductViewModel model = new ProductViewModel();
        model.categories = await _categoryService.GetCategories();
        model.users = await _userService.GetUsers();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Store(ProductViewModel product)
    {
        ViewData["nav"] = "product";
        if (!ModelState.IsValid)
        {
            Console.WriteLine("console loggin of errors");
            foreach (var key in ModelState.Keys)
            {
                var state = ModelState[key];
                if (state.Errors.Count > 0)
                {
                    Console.WriteLine($"Campo con error: {key}");
                    foreach (var error in state.Errors)
                    {
                        Console.WriteLine($" - Error: {error.ErrorMessage}");
                    }
                }
            }
            TempData["error"] = "El producto no fue almacenado!";
            return View("Create", product);
        }

        if (await _productService.CreateProduct(product))
            TempData["success"] = "El producto fue almacenado Exitosamente!";
        else
            TempData["error"] = "Ocurrio un error. El producto no fue almacenado!";

        return RedirectToAction(nameof(Index));
    }
    
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        ViewData["nav"] = "product";

        Product product = await _productService.GetProduct(id);

        ProductViewModel model = new ProductViewModel();
        model.ProductId = product.ProductId;
        model.Name = product.Name;
        model.Description = product.Description;
        model.Price = product.Price;
        model.ProductCategory = await _categoryService.GetCategory(product.CategoryId?? 0);
        model.ProductUser = await _userService.GetUser(product.UserId?? 0);
        model.UserId = product.UserId;

        return View(model);
    }

    [HttpGet("Product/Edit/{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {    
        ViewData["nav"] = "product";
        if (id is 0)
        {
            TempData["error"] = "El producto no fue encontrado!";
            return RedirectToAction(nameof(Index));
        }
        
        Product product = await _productService.GetProduct(id);
        
        ProductViewModel model = new ProductViewModel();
        model.ProductId = product.ProductId;
        model.Name = product.Name;
        model.Description = product.Description;
        model.Price = product.Price;
        model.UserId = product.UserId;
        model.CategoryId = product.CategoryId;

        model.categories = await _categoryService.GetCategories();
        model.users = await _userService.GetUsers();

        return View(model);        
    }

    [HttpPost]
    public async Task<IActionResult> Update(Product product)
    {
        ViewData["nav"] = "product";
        if (!ModelState.IsValid)
        {
            TempData["error"] = "El producto no fue actualizado!";
            return View("Edit", product);
        }
        
        if (await _productService.UpdateProduct(product))
        {
            TempData["success"] = "El producto fue actualizado Exitosamente!";
            return RedirectToAction(nameof(Index));
        }
        else
        {            
            TempData["error"] = "Ocurrio un error. El producto no fue actualizado!";
            return RedirectToAction(nameof(Index));
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        ViewData["nav"] = "product";        
        if (await _productService.DeleteProduct(id))
            TempData["success"] = "El producto fue eliminado Exitosamente!";
        else 
            TempData["error"] = "Ocurrio un error. El producto no fue eliminado!";
        
        return RedirectToAction(nameof(Index));
    }
}