using AutoMapper;
using GM_Warehouse.Components.Models.CreateModels;
using GM_Warehouse.Components.Models.ViewModels;
using GMWarehouse.CustomAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace GM_Warehouse.Components.Services;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Consumes("application/json")]
[ApiKeyAuthorize]
public class OrdersController : ControllerBase
{
    private readonly OrderService _orderService;
    private readonly IMapper _mapper;

    public OrdersController(OrderService orderService, IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves an order based on its ID.
    /// </summary>
    /// <param name="orderId">The unique identifier of the order.</param>
    /// <returns>The order details if found.</returns>
    /// <response code="200">Returns the order details.</response>
    /// <response code="404">If the order is not found.</response>
    [HttpGet("{orderId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrderFromId(Guid orderId)
    {
        var order = await _orderService.GetOrderFromId(orderId);
        if (order == null)
        {
            return NotFound(new { Message = "Order not found." });
        }
        return Ok(order);
    }

    /// <summary>
    /// Creates a new order.
    /// </summary>
    /// <param name="order">The details of the order to create.</param>
    /// <returns>The created order's details and its URI.</returns>
    /// <response code="201">Returns the created order.</response>
    /// <response code="400">If the input data is invalid.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OrderViewModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateOrder([FromBody] OrderCreateModel order)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdOrder = await _orderService.CreateOrder(order);
        return CreatedAtAction(nameof(GetOrderFromId), new { orderId = createdOrder.OrderId }, createdOrder);
    }

    /// <summary>
    /// Retrieves all orders.
    /// </summary>
    /// <returns>A list of all orders in the system.</returns>
    /// <response code="200">Returns a list of orders.</response>
    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OrderViewModel>))]
    public async Task<IActionResult> GetAllOrders()
    {
        var orders = await _orderService.GetAllOrders();
        return Ok(orders);
    }

    /// <summary>
    /// Retrieves all orders placed by a specific salesman.
    /// </summary>
    /// <param name="salesmanId">The unique identifier of the salesman.</param>
    /// <returns>A list of orders associated with the salesman.</returns>
    /// <response code="200">Returns a list of orders for the salesman.</response>
    /// <response code="404">If no orders are found for the given salesman.</response>
    [HttpGet("salesman/{salesmanId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderViewModel>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrderFromSalesman(Guid salesmanId)
    {
        var orders = await _orderService.GetOrdersFromSalesman(salesmanId);
        if (!orders.Any())
        {
            return NotFound(new { Message = "No orders found for the given salesman." });
        }
        return Ok(orders);
    }
}
