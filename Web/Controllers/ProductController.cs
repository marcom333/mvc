using Application.Entities;
using Application.Interface.Service;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModel;
namespace Web.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IUserService _userService;

    public ProductController(IProductService productService, ICategoryService categoryService, IUserService userService)
    {
        _productService = productService;
        _categoryService = categoryService;
        _userService = userService;
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
        return View(p);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = await _categoryService.GetCategories();
        ViewBag.Users = await _userService.GetUsers(null);
        return View(new ProductCreateViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductCreateViewModel p){
        if(!ModelState.IsValid){
            ViewBag.Categories = await _categoryService.GetCategories();
            ViewBag.Users = await _userService.GetUsers(null);
            return View(p);
        }
        Product product = await _productService.CreateProduct(new Product() {
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            CategoryId = p.CategoryId,
            UserId = p.UserId
        });
        return RedirectToAction("Detail", new {id=product.ProductId});
    }

    // [HttpGet]
    // public async Task<IActionResult> Update(int id){
    //     Product? p = await _productService.GetProduct(id);
    //     if(p == null) return NotFound();
    //     ViewBag.Categories = await _categoryService.GetCategories();
    //     ViewBag.Users = await _userService.GetUsers(null);
    //     return View(p);
    // }

    [HttpGet]
    public async Task<IActionResult> Update(int id){
        Product? p = await _productService.GetProduct(id);
        if(p == null) return NotFound();
        ProductCreateViewModel pvm = new()
        {
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            CategoryId = p.CategoryId,
            UserId = p.UserId
        };
        ViewBag.Categories = await _categoryService.GetCategories();
        ViewBag.Users = await _userService.GetUsers(null);
        return View(pvm);
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

        return RedirectToAction("Detail", "Product", new{id});
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