using Application.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

// Product/
[Route("Product")]
public class ProductController : Controller {

    // Index
    [HttpGet("Index")]
    public IActionResult Index() {
        List<Product> products = [];

        if (TempData["error"] != null)
            ViewBag.Error = "No existen productos en esa categoria";

        for (int i = 0; i < 10; i++) {
            products.Add(new() {
                Name = "Product #" + i
            });
        }
        return View(products);
    }

    public IActionResult Detail(int id) {
        if (id == 50) {
            TempData["error"] = true;
            return RedirectToAction("Index");
        }

        if (id == 100)
            return NotFound();
        if (id == 5)
            ViewBag.Warning = "Casi se terminan!!";

        return View(new Product() {
            Name = "Detail product " + id,
            Description = "Test product",
            Price = 1,
            CategoryId = 1,
            UserId = 1
        });
    }
    [HttpGet]
    public IActionResult Create() { //get por defecto, [HttpGet] si falla
        return View(); //Product/Create.cshtml
    }
    [HttpPost]
    public IActionResult Create(Product p) {
        if (p.Name == "") return BadRequest();
        if (p.Price == 0) return BadRequest();
        if (p.CategoryId == 0) return BadRequest();
        if (p.UserId == 0) return BadRequest();

        return RedirectToAction("Detail", new { id = 1 });
    }

    public IActionResult Update(int id) {
        if (id == 100) return NotFound();
        return View(new Product() {
            Name = "Detail product " + id,
            Description = "Test product",
            Price = 1,
            CategoryId = 1,
            UserId = 1
        });
    }
    [HttpPost]
    public IActionResult Update(int id, Product p) {
        if (id == 100) return NotFound();
        if (p.Name == "") return BadRequest();
        if (p.Price == 0) return BadRequest();
        if (p.CategoryId == 0) return BadRequest();
        if (p.UserId == 0) return BadRequest();

        return RedirectToAction("Detail", new { id });
    }

    public ViewResult ViewResult()
    {
        return View();
    }

    public JsonResult JsonResult()
    {
        return Json(new
        {
            Name = "Diana",
            Date = DateTime.Now
        });
    }

    public RedirectResult RedirectResult()
    {
        //externos
        return Redirect("https://www.google.com");
    }

    public RedirectToActionResult RedirectToActionResult()
    {
        //internos
        return RedirectToAction("Index", "Home", new { Id = 1 });
    }

    public ContentResult ContentResult() {
        return Content("Hello world");
    }

    public NotFoundResult NotFoundResult() {
        return NotFound();
    }

    public OkObjectResult OkObjectResult() {
        return Ok(new { });
    }

    public BadRequestResult BadRequestResult() {
        return BadRequest();
    }
}
