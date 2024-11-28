using GM_Warehouse.Components.Models.ViewModels;

namespace GM_Warehouse.Components.Services.Interfaces;

public interface ISupplierService
{
    public Task<IEnumerable<SupplierViewModel>> GetAllSuppliers();
    public Task<SupplierViewModel?> GetSupplierByIdAsync(Guid id);

}