using System.Threading.Tasks;
using Application.Entities;
using Application.Interface.Service;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModel;

namespace Web.Controllers.Api;

[Route("Api/Product")]
[ApiController]
public class ProductApiController : ControllerBase{

    private readonly IProductService _productService;

    public ProductApiController(IProductService productService) {
        _productService = productService;
    }
    
    [HttpGet]
    public async Task<ActionResult> Index([FromQuery] int page = 1){
        return Ok(await _productService.GetAllWithPage(page, 5));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Detail(int id) {
        Product ? p = await _productService.GetProduct(id);
        if(p != null)
            return Ok(p);
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateViewModel p) {
        if(ModelState.IsValid)
            return Ok(await _productService.CreateProduct(
                new Product() {
                    Name = p.Name,
                    CategoryId = p.CategoryId,
                    Description = p.Description,
                    Price = p.Price,
                    UserId = p.UserId
                }
            ));
        else
            return BadRequest(ModelState);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProductCreateViewModel p) {
        if (ModelState.IsValid) {
            Product product = new Product() {
                    ProductId = id,
                    Name = p.Name,
                    CategoryId = p.CategoryId,
                    Description = p.Description,
                    Price = p.Price,
                    UserId = p.UserId
                };
            await _productService.UpdateProduct(product);
            return Ok(new {status="ok", msg="La actualización fue exitosa", obj=product});
        }
        else
            return BadRequest(ModelState);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) {
        Product? p = await _productService.GetProduct(id);
        if(p == null)
            return NotFound();
        
        await _productService.DeleteProduct(p);
        return Ok(ApiResponseViewModel.Ok("Se ha eliminado con exito", p));
    }


}
