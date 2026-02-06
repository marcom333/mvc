using System.Threading.Tasks;
using Application.Interface.Service;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web;

[Route("Api/Product"), ApiController, AllowAnonymous]
public class ProductApiController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductApiController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        return Ok(await _productService.GetProducts());
    }
}    