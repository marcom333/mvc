using Application.Entities;
using Microsoft.AspNetCore.Mvc;
namespace Web.Controllers;

public class ProductController : Controller
{
    [HttpGet]
    public IActionResult Index(List<Product> products)
    {
        products.ForEach(x =>
        {
            Console.WriteLine("Nombre: " + x.Name + " Descripción: " + x.Description + " Precio: " + x.Price + " ID Categoría: " + x.CategoryId + " ID Usuario: " + x.UserId);
        });
        Console.WriteLine("===========================================================================================================================================================");
        Console.WriteLine("===========================================================================================================================================================");

        products ??= new List<Product>();

        if (products.Count == 0)
        {
            for (int i = 0; i < 10; i++)
            {
                products.Add(new Product
                {
                    Name = "Product # " + i,
                    CategoryId = i
                });
            }
        }

        if(TempData["error"] != null)
        {
            ViewBag.error = "No existen más productos de esa categoría.";
        }

        return View(products);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        return View(new Product()
        {
            Name = "Product " + id,
            Description = "Test Product",
            Price = 1,
            CategoryId = id,
            UserId = 1
        });
    }

    [HttpPost]
    public IActionResult Update(Product model)
    {
        List <Product> products = [];

        for(int i=0; i<10; i++)
        {
            if(i == model.CategoryId)
            {
                products.Add(new()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    CategoryId = i
                });
            } 
            else
            {
                products.Add(new()
                {
                    Name = "Product # " + i,
                    Description = "Test Product",
                    Price = 1,
                    CategoryId = i,
                    UserId = 1
                });
            }
        }
        products.ForEach(x =>
        {
            Console.WriteLine("Nombre: " + x.Name + " Descripción: " + x.Description + " Precio: " + x.Price + " ID Categoría: " + x.CategoryId + " ID Usuario: " + x.UserId);
        });
        Console.WriteLine("===========================================================================================================================================================");
        Console.WriteLine("===========================================================================================================================================================");
        TempData["saved_changes"] = "Los cambios han sido guardados.";
        return RedirectToAction("Index");
    }

    public IActionResult Detail(int id)
    {
        if(id == 100)
            return NotFound();

        if(id == 50)
        {
            TempData["error"] = true; //No mandar un objeto cuando se use TempData
            return RedirectToAction("Index");
        };

        if(id == 5)
        {
            ViewBag.Warning = "Casi se terminan!!";
        }

        return View(new Product()
        {
            Name = "Product " + id,
            Description = "Test Product",
            Price = 1,
            CategoryId = 1,
            UserId = 1
        });
    }
    
    public ViewResult ViewResult()
    {
        return View();
    }

    public JsonResult JsonResult()
    {
        return Json(new
        {
            Name = "Hello",
            DateOnly = DateTime.Now
        });
    }

    public RedirectResult RedirectResult()
    {
        return Redirect("http://google.com");
    }

    public RedirectToActionResult RedirectToActionResult()
    {
        return RedirectToAction("Index", "Home", new {Id=1});
    }

    public ContentResult ContentResult()
    {
        return Content("Hello World");
    }

    public NotFoundResult NotFoundResult()
    {
        return NotFound();
    }

    public OkObjectResult OkObjectResult()
    {
        return Ok(new {});
    }

    public BadRequestResult BadRequestResult()
    {
        return BadRequest();
    }
}