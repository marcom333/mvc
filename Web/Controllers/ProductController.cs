

using Application.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

// Product/
public class ProductController : Controller {
    
    // Index
    public IActionResult Index() {
        List<Product> products = [];

        if(TempData["error"] != null)
            ViewBag.Error = "No existen más productos de esa categoría";

        for(int i = 0; i<10; i++) {
            products.Add(new() {
               Name = "Product #"+i 
            });
        }
        return View(products);
    }

    public IActionResult Detail(int id) {
        ViewBag.status= TempData["status"];
        if(id == 50) {
            TempData["error"] = true;
            return RedirectToAction("Index");
        }

        if(id == 100)
            return NotFound();
        if(id == 5)
            ViewBag.Warning = "Casi se terminan!!";

        return View(new Product() {
            Name = "Detail product "+id,
            Description = "Test product",
            Price = 1,
            CategoryId = 1,
            UserId = 1
        });
    }

    public IActionResult Create() {//get por defecto, [httpGet] si falla
        return View(); //Product/Create.cshtml
    }
    [HttpPost]
    public IActionResult Create(Product p) {
        if(p.Name == "") return BadRequest();
        if(p.Price == 0) return BadRequest();
        if(p.CategoryId == 0) return BadRequest();
        if(p.UserId == 0) return BadRequest();
        TempData["status"]= 200;

        return RedirectToAction("Detail", new {id=1});
    }

    public IActionResult Update(int id) {
        if(id == 100) return NotFound();
        return View(new Product() {
            Name = "Detail product "+id,
            Description = "Test product",
            Price = 1,
            CategoryId = 1,
            UserId = 1
        });
    }
    [HttpPost]
    public IActionResult Update(int id, Product p) {
        if(id == 100) return NotFound();
        if(p.Name == "") return BadRequest();
        if(p.Price == 0) return BadRequest();
        if(p.CategoryId == 0) return BadRequest();
        if(p.UserId == 0) return BadRequest();

        return RedirectToAction("Detail", new {id});
    }


    public ViewResult ViewResult() {
        return View();
    }

    public JsonResult JsonResult() {
        return Json(new {
            Name="Hello",
            Date = DateTime.Now
        });
    }

    public RedirectResult RedirectResult() {
        return Redirect("http://www.google.com");
    }

    public RedirectToActionResult RedirectToActionResult() {
        return RedirectToAction("Index", "Home", new {Id=1});
    }

    public ContentResult ContentResult() {
        return Content("Hello world");
    }

    public NotFoundResult NotFoundResult() {
        return NotFound();
    }

    public OkObjectResult OkObjectResult() {
        return Ok(new {});
    }

    public BadRequestResult BadRequestResult() {
        return BadRequest();
    }
}