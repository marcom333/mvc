using System.Threading.Tasks;
using Application.Entities;
using Application.Interface.Service;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        ViewData["nav"] = "product";
        List<Product> products = await _productService.GetProducts();
        return View(products);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewData["nav"] = "product";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Store(Product product)
    {
        ViewData["nav"] = "product";
        if (!ModelState.IsValid)
        {
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

        ProductDetailViewModel detail = new();
        //Busqueda de producto
        detail.Product = await _productService.GetProduct(id);

        detail.Category = new Category()
        {
            CategoryId = 1,
            Name = "Cat 1"
        };

        detail.User = new User()
        {
          UserId = 1,
          Name = "César"  
        };

        return View(detail);
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

        return View(await _productService.GetProduct(id));        
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