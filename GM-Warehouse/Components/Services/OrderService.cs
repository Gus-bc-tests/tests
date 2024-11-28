using System.Diagnostics;
using AutoMapper;
using GM_Warehouse.Components.Models.CreateModels;
using GM_Warehouse.Components.Models.DataModels;
using GM_Warehouse.Components.Models.Enums;
using GM_Warehouse.Components.Models.ViewModels;
using GM_Warehouse.Components.Services.Interfaces;
using GM_Warehouse.Data;
using Microsoft.EntityFrameworkCore;

namespace GM_Warehouse.Components.Services;

public class OrderService : IOrderService
{
    private readonly DataBaseContext _dbContext;
    private readonly IMapper _mapper;

    public OrderService(DataBaseContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    // Method to create an order
    public async Task<OrdersDataModel> CreateOrder(OrderCreateModel model)
    {
        var newOrder = _mapper.Map<OrdersDataModel>(model);
        
        newOrder.OrderId = Guid.NewGuid(); 
        newOrder.OrderItemDataModelIds = [];

        decimal totalPrice = 0;
        
        var orderItems = new List<OrderItemDataModel>();
        foreach (var orderItemCreate in model.OrderItems)
        {
            var product = await _dbContext.Products.FindAsync(orderItemCreate.ProductId);
            
            if (product == null)
            {
                Debug.WriteLine($"Product with ID {orderItemCreate.ProductId} not found.");
                continue;
            } else
            {
               
                var orderItem = _mapper.Map<OrderItemDataModel>(orderItemCreate);
                orderItem.OrderItemId = Guid.NewGuid();
                orderItem.OrderId = newOrder.OrderId;
                newOrder.OrderItemDataModelIds.Add(orderItem.OrderItemId);

               
                totalPrice += product.Price * orderItemCreate.Quantity;
                orderItems.Add(orderItem);
            }
        }

        // Apply discount
        totalPrice -= totalPrice * model.Discount / 100;
        
        newOrder.TotalPrice = decimal.Round(totalPrice, 2);

        await _dbContext.OrderItems.AddRangeAsync(orderItems);

        await _dbContext.Orders.AddAsync(newOrder);
        await _dbContext.SaveChangesAsync();

        return newOrder;
    }

    public async Task<bool> ChangeOrderStatusById(State status, Guid orderId)
    {
        var order = await _dbContext.Orders.FindAsync(orderId);
        if (order == null) return false;
        order.Status = status;
        return await _dbContext.SaveChangesAsync() > 0;
    }
    
    // Method to get all orders and return as ViewModels
    public async Task<IEnumerable<OrderViewModel>> GetAllOrders()
    {
        // Load all orders
        var orders = await _dbContext.Orders.ToListAsync();
        if (orders == null || orders.Count == 0)
            return Enumerable.Empty<OrderViewModel>();

        // Map OrdersDataModel to OrderViewModel
        var orderViewModelTasks = orders.Select(async order =>
        {
            if (order == null) return null; // Guard against null order

            OrderViewModel orderViewModel = _mapper.Map<OrderViewModel>(order);

            // Map Customer and Salesman with null checks
            CustomerDataModel? _customerData = await _dbContext.Customers.FindAsync(order.CustomerId);
            if (_customerData != null)
            {
                orderViewModel.Customer = _mapper.Map<CustomerViewModel>(_customerData);
                orderViewModel.Customer.ContactPerson = _mapper.Map<UserViewModel>(await _dbContext.Users.FindAsync(_customerData.ContactPersonId));
                orderViewModel.Customer.Manager = _mapper.Map<UserViewModel>(await _dbContext.Users.FindAsync(_customerData.ManagerId));
            }

            if (order.SalesmanId != null)
            {
                orderViewModel.Salesman = _mapper.Map<UserViewModel>(await _dbContext.Users.FindAsync(order.SalesmanId));
            }

            List<OrderItemViewModel> orderItemViewModels = new List<OrderItemViewModel>();

            if (order.OrderItemDataModelIds != null)
            {
                foreach (Guid _orderItemId in order.OrderItemDataModelIds)
                {
                    OrderItemDataModel? _orderItemData = await _dbContext.OrderItems.FirstOrDefaultAsync(x => x.OrderItemId == _orderItemId);
                    if (_orderItemData != null)
                    {
                        OrderItemViewModel _orderItemView = _mapper.Map<OrderItemViewModel>(_orderItemData);
                        _orderItemView.Product = _mapper.Map<ProductViewModel>(await _dbContext.Products.FindAsync(_orderItemData.ProductId));
                        _orderItemView.Order = orderViewModel;
                        orderItemViewModels.Add(_orderItemView);
                    }
                }
            }

            orderViewModel.OrderItems = orderItemViewModels;
            return orderViewModel;
        });

        // Await all tasks, filter out nulls, and return the result
        var orderViewModels = await Task.WhenAll(orderViewModelTasks);
        return orderViewModels.Where(vm => vm != null);
    }



    // Method to get an order by its ID
    public async Task<OrderViewModel?> GetOrderFromId(Guid id)
    {
        var _orders = await GetAllOrders();
        return _orders.FirstOrDefault(order => order.OrderId == id);
    }
    
    // Method to get orders made by a specific salesman
    public async Task<IEnumerable<OrderViewModel>> GetOrdersFromSalesman(Guid salesmanId)
    {
        var _orders = await GetAllOrders();
        return _orders.Where(order => order.Salesman.UserId == salesmanId);
    }

}