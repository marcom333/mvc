

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
    public async Task<IActionResult> Index() {
        if(TempData["error"] != null)
            ViewBag.Error = "No existen más productos de esa categoría";
        return View(await _productService.GetProducts());
    }
    
    [HttpGet("Detail/{id}")]
    public IActionResult Detail(int id) {
        Product? p = _productService.GetProduct(id);
        if(p == null) return NotFound();

        ProductDetailViewModel detail = new();
        detail.Product = p;
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
        return View(new Product() {
            CategoryId=1,
            UserId=1
        });
    }
    [HttpPost("Create")]
    public IActionResult Create(Product p) {
        if(p.Name == "") return BadRequest();
        if(p.Price == 0) return BadRequest();
        if(p.CategoryId == 0) return BadRequest();
        if(p.UserId == 0) return BadRequest();
        TempData["status"] = 200;
        Product product = _productService.CreateProduct(p);

        return RedirectToAction("Detail", new {id=product.ProductId});
    }

    // Product/Update/123
    [HttpGet("Update/{id}")]
    public IActionResult Update(int id) {
        Product? p = _productService.GetProduct(id);
        if(p == null) return NotFound();
        return View(p);
    }
    [HttpPost("Update/{id}")]
    public IActionResult Update(int id, Product p) {
        p.ProductId = id;
        if(p.Name == "") return BadRequest();
        if(p.Price == 0) return BadRequest();
        if(p.CategoryId == 0) return BadRequest();
        if(p.UserId == 0) return BadRequest();
        _productService.UpdateProduct(p);

        return RedirectToAction("Detail", "Product", new {id, name="hola", registrado=true});
    }

    [HttpPost("Delete/{id}")]
    public IActionResult Delete(int id) {
        Product? p = _productService.GetProduct(id);
        if(p == null) return NotFound();
        _productService.DeleteProduct(p);
        return RedirectToAction("Index");
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
        return Ok(new {}); // 200
    }

    public BadRequestResult BadRequestResult() {
        return BadRequest(); //400
    }
}