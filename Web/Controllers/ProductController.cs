using Application.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class ProductController : Controller
{
    public IActionResult Index()
    {
        List<Product> productos = new List<Product>();
        for (int i = 0; i < 10; i++)
        {
            productos.Add(new()
            {
                Name = $"Product #{i + 1}"
            });
        }
        return View(productos);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

   
    [HttpGet("Product/Edit/{id:int}")]
    public IActionResult Edit(int? id)
    {
        Console.WriteLine($"{DateTime.Now} | {Request.Method} | {Request.Path} | id={id}");

        if (id == null)
        {
            TempData["error"] = "El producto no Existe";
            return RedirectToAction(nameof(Index));
        }

        //Busqueda de producto
        Product producto = new Product()
        {
            Name = "PC",
            Description = "Torre",
            Price = 10,
            CategoryId = 5,
            UserId = 10
        };

        return View(producto);
    }

}