using Microsoft.AspNetCore.Mvc;

namespace GM_Warehouse.Components.Services;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }
    
}
