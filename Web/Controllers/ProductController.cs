using Microsoft.AspNetCore.Mvc;
using Application.Entities;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Web.Controllers;

public class ProductController : Controller
{
    
    public IActionResult Index()
    {
        List<Product> products = [];

        if(TempData["error"] != null)
        {
            ViewBag.Error = "No existen mas productos de esa categoria";
        }

        for(int i = 0; i<10; i++)
        {
            products.Add(new()
            {
                Name = "Prodcut #"+i
            });
        }
        return View(products);
    }

    public IActionResult Detail(int id)
    {
        if(id == 50){
            TempData["error"] = true;
            return RedirectToAction("Index");
        }
        if(id == 100)
        {
            return NotFound();
        }
        if(id == 5)
        {
            ViewBag.Warning = "Caso se termina!";
        }
        return View(new Product()
        {
            Name = "Detail product" + id,
            Description = "Test product",
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
            Date = DateTime.Now
        });
    }

    public RedirectResult RedirectResult()
    {
        return Redirect("http://www.google.com");
    }

    public RedirectToActionResult RedirectToActionResult()
    {
        return RedirectToAction("Index", "Home", new {Id = 1});
    }

    public ContentResult ContentResult()
    {
        return Content("Hello Word");
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