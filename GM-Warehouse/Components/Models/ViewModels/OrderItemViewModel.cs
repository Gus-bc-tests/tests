namespace GM_Warehouse.Components.Models.ViewModels;

public class OrderItemViewModel
{
    public Guid OrderItemId { get; set; }
        
    public ProductViewModel Product { get; set; }
        
    public OrderViewModel Order { get; set; }

    public int Quantity { get; set; }
        
    // TODO: Make toString()
}