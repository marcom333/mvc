using Application.Entities;
using Microsoft.AspNetCore.Mvc;
namespace Web.Controllers;

public class ProductController : Controller
{
    [HttpGet]
    public IActionResult Index(List<Product>? products)
    {
        if(products == null)
        {
            for(int i=0; i<10; i++)
            {
                products =
                [
                    new()
                    {
                        Name = "Product # "+i,
                        CategoryId = i
                    },
                ];
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

    [HttpPost]
    // public IActionResult Create()
    // {
        
    // }

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
            if(model.CategoryId == i)
                {
                    products.Add(new()
                    {
                        Name = "Product # "+i,
                            CategoryId = i
                    });
                } 
                else
                {
                    products.Add(new()
                    {
                        Name = model.Name,
                        Description = model.Description,
                        Price = model.Price,
                    });
                }
            
        }
        return RedirectToAction("Index", products);
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