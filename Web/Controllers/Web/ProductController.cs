using Application.Entities;
using Application.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Web.Filters;
using Web.ViewModel;

namespace Web.Controllers.Web;

// Product/
[Route("Product")]
// [Authorize]
public class ProductController : Controller {

    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IUserService _userService;
    private readonly IMemoryCache _memoryCache;

    public ProductController(IProductService productService, ICategoryService categoryService, IUserService userService, IMemoryCache memoryCache) {
        _productService = productService;
        _categoryService = categoryService;
        _userService = userService;
        _memoryCache = memoryCache;
    }
    
    // Index
    [HttpGet("Index")]
    // [AllowAnonymous]
    public async Task<IActionResult> Index() {
        if(TempData["error"] != null)
            ViewBag.Error = "No existen más productos de esa categoría";
        if(!_memoryCache.TryGetValue("products", out List<Product> products)) {
            products = await _productService.GetProducts();
            int longvar = 300000;
            while(longvar-- != 0){}
            _memoryCache.Set("products", products, TimeSpan.FromMinutes(5));
        }
        return View(products);
    }
    
    [HttpGet("Detail/{id}", Name ="ProductDetails")]
    [ResponseCache(Duration = 60, VaryByQueryKeys = new[] {"id"})]
    public async Task<IActionResult> Detail(int id) {
        Product? p = await _productService.GetProduct(id);
        if(p == null) return NotFound();
        p.Name += " " + DateTime.Now;
        return PartialView(p);
    }
    [HttpGet("Create")]
    public async Task<IActionResult> Create() { // get por defecto, [HttpGet] si falla
        ViewBag.Categories = await _categoryService.GetCategorys();
        ViewBag.Users = await _userService.GetUsers(null);
        return View(new ProductCreateViewModel());
    }
    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductCreateViewModel p) {
        if(!ModelState.IsValid){
            ViewBag.Categories = await _categoryService.GetCategorys();
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

    // Product/Update/123
    [HttpGet("Update/{id}")]
    [Authorize(Policy = IsAdminRequirement.PolicyName)]
    public async Task<IActionResult> Update(int id) {
        Product? p = await _productService.GetProduct(id);
        if(p == null) return NotFound();
        ViewBag.Categories = await _categoryService.GetCategorys();
        ViewBag.Users = await _userService.GetUsers(null);
        return PartialView(new ProductCreateViewModel() {
            Name = p.Name,
            CategoryId = p.CategoryId,
            Description = p.Description,
            Price = p.Price,
            UserId = p.UserId
        });
    }
    [HttpPost("Update/{id}")]
    public async Task<IActionResult> Update(int id, Product p) {
        p.ProductId = id;
        if(p.Name == "") return BadRequest();
        if(p.Price == 0) return BadRequest();
        if(p.CategoryId == 0) return BadRequest();
        if(p.UserId == 0) return BadRequest();
        await _productService.UpdateProduct(p);

        return RedirectToAction("Index", "Product", new {id});
    }

    [HttpPost("Delete/{id}")]
    public async Task<IActionResult> Delete(int id) {
        Console.WriteLine(id);
        Product? p = await _productService.GetProduct(id);
        if(p == null) return NotFound();
        await _productService.DeleteProduct(p);
        return RedirectToAction("Index");
    }

    [HttpGet("Index2")]
    public async Task<IActionResult> IndexTwo([FromQuery] int page = 1, [FromQuery] string? name = null) {
        PageResult<Product> model = await _productService.GetAllWithPage(page, 5, name);
        ViewBag.Name = name;
        return View(model);
    }
}