

using System.Threading.Tasks;
using Application.Entities;
using Application.Interface.Service;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModel;

namespace Web.Controllers;

// Product/
[Route("Product")]
public class ProductController : Controller {

    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IUserService _userService;

    public ProductController(IProductService productService, ICategoryService categoryService, IUserService userService) {
        _productService = productService;
        _categoryService = categoryService;
        _userService = userService;
    }
    
    // Index
    [HttpGet("Index")]
    public async Task<IActionResult> Index() {
        if(TempData["error"] != null)
            ViewBag.Error = "No existen más productos de esa categoría";
        return View(await _productService.GetProducts());
    }
    
    [HttpGet("Detail/{id}")]
    public async Task<IActionResult> Detail(int id) {
        Product? p = await _productService.GetProduct(id);
        if(p == null) return NotFound();
        return View(p);
    }
    [HttpGet("Create")]
    public async Task<IActionResult> Create() { // get por defecto, [HttpGet] si falla
        ViewBag.Categories = await _categoryService.GetCategorys();
        ViewBag.Users = await _userService.GetUsers();
        return View(new Product());
    }
    [HttpPost("Create")]
    public async Task<IActionResult> Create(Product p) {
        if(p.Name == "") return BadRequest();
        if(p.Price == 0) return BadRequest();
        if(p.CategoryId == 0) return BadRequest();
        if(p.UserId == 0) return BadRequest();
        TempData["status"] = 200;
        Product product = await _productService.CreateProduct(p);

        return RedirectToAction("Detail", new {id=product.ProductId});
    }

    // Product/Update/123
    [HttpGet("Update/{id}")]
    public async Task<IActionResult> Update(int id) {
        Product? p = await _productService.GetProduct(id);
        if(p == null) return NotFound();
        ViewBag.Categories = await _categoryService.GetCategorys();
        ViewBag.Users = await _userService.GetUsers();
        return View(p);
    }
    [HttpPost("Update/{id}")]
    public async Task<IActionResult> Update(int id, Product p) {
        p.ProductId = id;
        if(p.Name == "") return BadRequest();
        if(p.Price == 0) return BadRequest();
        if(p.CategoryId == 0) return BadRequest();
        if(p.UserId == 0) return BadRequest();
        await _productService.UpdateProduct(p);

        return RedirectToAction("Detail", "Product", new {id});
    }

    [HttpPost("Delete/{id}")]
    public async Task<IActionResult> Delete(int id) {
        Product? p = await _productService.GetProduct(id);
        if(p == null) return NotFound();
        await _productService.DeleteProduct(p);
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