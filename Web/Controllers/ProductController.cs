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

   
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Store(Product product) 
    {
        if (ModelState.IsValid)
        {
            // Aquí iría la lógica para guardar en base de datos
            // _repository.Add(product);
            // _repository.Save();
            
            return RedirectToAction("Index");
        }
        return View(product);
    }

    public IActionResult Edit(int id)
    {
        // Simulación de buscar producto por ID en BD
        Product product = new Product
        {
            Id = id,
            Name = "Producto Demo",
            Description = "Descripción demo",
            Price = 99
        };
        
        return View(product);
    }

    [HttpPost]
    public IActionResult Update(Product product)
    {
        if (ModelState.IsValid)
        {
            // Aquí iría la lógica para actualizar en base de datos
            // _repository.Update(product);
            // _repository.Save();
            
            return RedirectToAction("Index");
        }
        return View(product);
    }
}