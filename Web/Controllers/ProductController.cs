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

    public IActionResult Index()
    {
        ViewData["nav"] = "product";
        return View(_productService.GetProducts());
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewData["nav"] = "product";
        return View();
    }

    [HttpPost]
    public IActionResult Store(Product product)
    {
        ViewData["nav"] = "product";
        if (!ModelState.IsValid)
        {
            TempData["error"] = "El producto no fue almacenado!";
            return View("Create", product);
        }

        if (_productService.CreateProduct(product))
        {
            TempData["success"] = "El producto fue almacenado Exitosamente!";
            return RedirectToAction(nameof(Index));
        }
        else
        {            
            TempData["error"] = "Ocurrio un error. El producto no fue almacenado!";
            return RedirectToAction(nameof(Index));
        }
    }
    
    [HttpGet]
    public IActionResult Details()
    {
        ViewData["nav"] = "product";

        ProductDetailViewModel detail = new();
        //Busqueda de producto
        detail.Product = new Product()
        {
            Name = "Lata de Verduras",
            Description = "Asi es, es una lata de verduras",
            Price = 10,
            CategoryId = 5, 
            UserId = 10
        };

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
    public IActionResult Edit(int id)
    {    
        ViewData["nav"] = "product";
        if (id is 0)
        {
            TempData["error"] = "El producto no fue encontrado!";
            return RedirectToAction(nameof(Index));
        }

        return View(_productService.GetProduct(id));        
    }

    [HttpPost]
    public IActionResult Update(Product product)
    {
        ViewData["nav"] = "product";
        if (!ModelState.IsValid)
        {
            TempData["error"] = "El producto no fue actualizado!";
            return View("Edit", product);
        }
        
        if (_productService.UpdateProduct(product))
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
}