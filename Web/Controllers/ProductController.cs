using Application.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("Product")]
public class ProductController : Controller
{
    [HttpGet("Index")]
    public IActionResult Index()
    {
        List<Product> products = [];
        for(int i =0; i<10; i++)
        {
            products.Add(new Product
            {
                Name="Product # "+i.ToString()
            });
        }

        return View(products);
    }

    [HttpGet("Detail/{id}")]
    public IActionResult Detail(int id)
    {
        if(id==100)
            return NotFound();
        return View(new Product()
        {
            Name="Nombre",
            Description="Test",
            Precio=19,
            CategoryId=1,
            UserId=1
        });
    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("Create")]
    public IActionResult Create(Product p)
    {
        /*if(p!=null)
            return BadRequest();*/
        
            return RedirectToAction("Detail", new {id=1});
    }

    [HttpGet("Edit/{id}")]
    public IActionResult Edit(int id)
    {
        if(id==15)
            return NotFound();
        
        return View(new Product()
        {
            Name ="Nombre"+id,
            Description="Producto",
            Precio=14,
            CategoryId=3,
            UserId=2
        });
    }

    [HttpPost("Edit/{id}")]
    public IActionResult Edit(int id, Product p)
    {
        /*if(p!=null)
            return BadRequest();*/

        return RedirectToAction("Detail", "Product", new {id, name="hola", registrado=true});
    }

}