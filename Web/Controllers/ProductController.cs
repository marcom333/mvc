using Application.Entities;
using Application.Interface.Service;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModel;
namespace Web.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        if(TempData["error"] != null)
            ViewBag.error = "No existen más productos de esa categoría.";
        return View(await _productService.GetProducts());
    }

    public async Task<IActionResult> Detail(int id)
    {
        Product? p = await _productService.GetProduct(id);
        if(p == null) return NotFound();

        Console.WriteLine("Category: " + p.CategoryId + " " + p.CategoryName + " " + p.CategoryDescription);

        ProductDetailViewModel detail = new();
        detail.Product = p;
        detail.Category.CategoryId = p.Category.CategoryId;
        detail.Category.Name = p.Category.Name;
        detail.User = new User()
        {
            Name = "Josh",
            UserId = 1
        };
        return View(detail);
    }

    public IActionResult Create()
    {
        return View(new Product(){
            CategoryId = 1,
            UserId = 1
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product p){
        if(p.Name == "") return BadRequest();
        if(p.Price == 0) return BadRequest();
        if(p.CategoryId == 0) return BadRequest();
        if(p.UserId == 0) return BadRequest();

        TempData["status"] = 200;
        Product product = await _productService.CreateProduct(p);

        return RedirectToAction("Detail", new {id = product.ProductId});
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id){
        Product? p = await _productService.GetProduct(id);
        if(p == null) return NotFound();
        return View(p);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, Product p)
    {
        p.ProductId = id;
        if(p.Name == "") return BadRequest();
        if(p.Price == 0) return BadRequest();
        if(p.CategoryId == 0) return BadRequest();
        if(p.UserId == 0) return BadRequest();
        await _productService.UpdateProduct(p);

        return RedirectToAction("Detail", "Product", new{id, name = "hola", registrado=true});
    }
    
     [HttpPost("Delete/{id}")]
    public async Task<IActionResult> Delete(int id) {
        Product? p = await _productService.GetProduct(id);
        if(p == null) return NotFound();
        await _productService.DeleteProduct(p);
        return RedirectToAction("Index");
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