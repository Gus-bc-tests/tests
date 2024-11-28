using GM_Warehouse.Components.Models.CreateModels;
using GM_Warehouse.Components.Models.DataModels;
using GM_Warehouse.Components.Models.ViewModels;
namespace GM_Warehouse.Components.Services.Interfaces;

public interface ICustomerService
{
    public Task<CustomerDataModel> AddCustomerAsync(CustomerCreateModel customer);
    public Task<IEnumerable<CustomerViewModel>> GetAllCustomers();

    public void DeleteAllCustomers();
}
