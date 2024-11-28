namespace GM_Warehouse.Components.Models.ViewModels;
public class SupplierOrderItemViewModel
{
    public Guid SupplierOrderItemId { get; set; }

    public SupplierOrdersViewModel SupplierOrder { get; set; }

    public ProductViewModel Product { get; set; }
    
    public int Quantity { get; set; }
    
    // TODO: Make toString()
}