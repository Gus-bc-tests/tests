using GM_Warehouse.Components.Models;
using GM_Warehouse.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GM_Warehouse.Components.Services;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class Suppliercontroller : Controller 
{
    private readonly SupplierService _supplierService;

    public Suppliercontroller(SupplierService supplierService)
    {
        _supplierService = supplierService;
    }
}