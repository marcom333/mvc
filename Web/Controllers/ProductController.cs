

using Application.Entities;
using Application.Interface.Service;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModel;

namespace Web.Controllers;

// Product/
[Route("Product")]
public class ProductController : Controller {

    private readonly IProductService _productService;

    public ProductController(IProductService productService) {
        _productService = productService;
    }
    
    // Index
    [HttpGet("Index")]
    public IActionResult Index() {
        if(TempData["error"] != null)
            ViewBag.Error = "No existen más productos de esa categoría";
        return View(_productService.GetProducts());
    }
    
    [HttpGet("Detail/{id}")]
    public IActionResult Detail(int id) {
        ViewBag.status = TempData["status"];
        if(id == 50) {
            TempData["error"] = true;
            return RedirectToAction("Index");
        }

        if(id == 100)
            return NotFound();
        if(id == 5)
            ViewBag.Warning = "Casi se terminan!!";

        ProductDetailViewModel detail = new();
        detail.Product = new Product() {
            Name = "<h1>Detail product "+id +"</h1>",
            Description = "Test product",
            Price = 1,
            CategoryId = 1,
            UserId = 1
        };
        detail.Category = new Category() {
            Name = "Verduras",
            CategoryId = 1
        };
        detail.User = new User() {
            Name = "Juan",
            UserId = 1
        };
        return View(detail);
    }
    [HttpGet("Create")]
    public IActionResult Create() { // get por defecto, [HttpGet] si falla
        return View(new Product());
    }
    [HttpPost("Create")]
    public IActionResult Create(Product p) {
        if(p.Name == "") return BadRequest();
        if(p.Price == 0) return BadRequest();
        if(p.CategoryId == 0) return BadRequest();
        if(p.UserId == 0) return BadRequest();
        TempData["status"] = 200;

        return RedirectToAction("Detail", new {id=1});
    }

    // Product/Update/123
    [HttpGet("Update/{id}")]
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
    [HttpPost("Update/{id}")]
    public IActionResult Update(int id, Product p) {
        if(id == 100) return NotFound();
        if(p.Name == "") return BadRequest();
        if(p.Price == 0) return BadRequest();
        if(p.CategoryId == 0) return BadRequest();
        if(p.UserId == 0) return BadRequest();

        return RedirectToAction("Detail", "Product", new {id, name="hola", registrado=true});
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
<<<<<<< HEAD
        return Ok(new {});
    }

    public BadRequestResult BadRequestResult() {
        return BadRequest();
=======
        return Ok(new {}); // 200
    }

    public BadRequestResult BadRequestResult() {
        return BadRequest(); //400
>>>>>>> b2ca0ee01653326a14ab742385c1abfdf336faba
    }
}