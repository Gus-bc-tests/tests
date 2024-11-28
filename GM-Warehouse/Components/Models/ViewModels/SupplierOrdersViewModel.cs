using GM_Warehouse.Components.Models.Enums;

namespace GM_Warehouse.Components.Models.ViewModels;

public class SupplierOrdersViewModel
{
    public Guid SupplierOrderId { get; set; } 

    public SupplierViewModel Supplier { get; set; }

    public DateTime OrderDate { get; set; }
    
    public List<SupplierOrderItemViewModel> SupplierOrderItems { get; set; } = [];

    public State Status { get; set; }
    
    // TODO: Make toString()
}