
using Microsoft.AspNetCore.Mvc;

namespace GM_Warehouse.Components.Services;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CustomerController : ControllerBase
{
    private readonly CustomerService _customerService;

    public CustomerController(CustomerService customerService)
    {
        _customerService = customerService;
    }
}