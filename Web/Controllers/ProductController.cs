using System.Threading.Tasks;
using Application.Entities;
using Application.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers;

//[Authorize]
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
            product.categories = await _categoryService.GetCategories();
            product.users = await _userService.GetUsers();
            TempData["error"] = "El producto no fue almacenado!";
            return View("Create", product);
        }

        Product p = new Product()
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            CategoryId = product.CategoryId,
            UserId = product.UserId
        };

        if (await _productService.CreateProduct(p))
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
        product.ProductCategory = await _categoryService.GetCategory(product.CategoryId?? 0);
        product.ProductUser = await _userService.GetUser(product.UserId?? 0);

        return View(product);
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
        
        ProductViewModel model = new ProductViewModel()
        {
            ProductId = product.ProductId,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            UserId = product.UserId,
            CategoryId = product.CategoryId,
            categories = await _categoryService.GetCategories(),
            users = await _userService.GetUsers(),
        };

        return View(model);        
    }

    [HttpPost]
    public async Task<IActionResult> Update(ProductViewModel product)
    {
        ViewData["nav"] = "product";
        if (!ModelState.IsValid)
        {
            product.categories = await _categoryService.GetCategories();
            product.users = await _userService.GetUsers();
            TempData["error"] = "El producto no fue actualizado!";
            return View("Edit", product);
        }
        
        Product p = new Product()
        {
            ProductId = product.ProductId,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            CategoryId = product.CategoryId,
            UserId = product.UserId
        };

        if (await _productService.UpdateProduct(p))
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

    //Logging errors
    // Console.WriteLine("console loggin of errors");
    // foreach (var key in ModelState.Keys)
    // {
    //     var state = ModelState[key];
    //     if (state?.Errors.Count > 0)
    //     {
    //         Console.WriteLine($"Campo con error: {key}");
    //         foreach (var error in state.Errors)
    //         {
    //             Console.WriteLine($" - Error: {error.ErrorMessage}");
    //         }
    //     }
    // }

}