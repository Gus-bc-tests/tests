using GM_Warehouse.Components.Models;
using GM_Warehouse.Components.Services.Interfaces;
using GM_Warehouse.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GM_Warehouse.Components.Services;
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class UserController : ControllerBase {
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }
    
}
