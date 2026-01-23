using Application.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class ProductController : Controller
{
    public IActionResult Index()
    {
        ViewData["nav"] = "product";
        List<Product> productos = new List<Product>();
        for(int i = 0; i < 10; i++)
        {
            productos.Add(new ()
            {
               Name = $"Product #{i+1}"
            });
        }
        return View(productos);
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

        TempData["success"] = "El producto fue almacenado Exitosamente!";
        return RedirectToAction(nameof(Index));
    }
    
    [HttpGet]
    public IActionResult Details()
    {
        ViewData["nav"] = "product";

        //Busqueda de producto
        Product producto = new Product()
        {
            Name = "Lata de Verduras",
            Description = "Asi es, es una lata de verduras",
            Price = 10,
            CategoryId = 5, 
            UserId = 10
        };

        return View(producto);
    }

    [HttpGet("Product/Edit/{id:int}")]
    public IActionResult Edit(int? id)
    {    
        ViewData["nav"] = "product";
        if (id == null)
        {
            TempData["error"] = "El producto no fue encontrado!";
            return RedirectToAction(nameof(Index));
        }

        //Busqueda de producto
        Product producto = new Product()
        {
            Name = "Lata de Verduras",
            Description = "Asi es, es una lata de verduras",
            Price = 10,
            CategoryId = 5, 
            UserId = 10
        };

        return View(producto);        
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

        TempData["success"] = "El producto fue actualizado Exitosamente!";
        return RedirectToAction(nameof(Index));
    }
}