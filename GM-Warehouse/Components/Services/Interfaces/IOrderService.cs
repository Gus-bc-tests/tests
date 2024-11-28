using GM_Warehouse.Components.Models.CreateModels;
using GM_Warehouse.Components.Models.DataModels;
using GM_Warehouse.Components.Models.ViewModels;

namespace GM_Warehouse.Components.Services.Interfaces;

public interface IOrderService
{
    public Task<OrdersDataModel> CreateOrder(OrderCreateModel model);
    
    public Task<IEnumerable<OrderViewModel>> GetAllOrders();
    
    public Task<OrderViewModel?> GetOrderFromId(Guid id);

    public Task<IEnumerable<OrderViewModel>> GetOrdersFromSalesman(Guid salesmanId);
}