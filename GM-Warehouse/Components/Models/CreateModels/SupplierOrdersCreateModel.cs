using GM_Warehouse.Components.Models.ViewModels;

namespace GM_Warehouse.Components.Models.CreateModels;

public class SupplierOrdersCreateModel
{
    public SupplierViewModel Supplier { get; set; }
    
    public List<SupplierOrderItemCreateModel> SupplierOrderItems { get; set; } = [];
}