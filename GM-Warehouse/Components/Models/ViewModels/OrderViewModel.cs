using GM_Warehouse.Components.Models.Enums;

namespace GM_Warehouse.Components.Models.ViewModels;

public class OrderViewModel
{
    public Guid OrderId { get; set; }
        
    public CustomerViewModel Customer { get; set; }
    
    public UserViewModel Salesman { get; set; }

    public DateTime OrderDate { get; set; }
    
    public State Status { get; set; }
    
    public List<OrderItemViewModel> OrderItems { get; set; } = [];

    public decimal TotalPrice { get; set; }

    public int Discount { get; set; }

    public bool ShowDetails { get; set; } = false;
    // TODO: Make toString()

}
