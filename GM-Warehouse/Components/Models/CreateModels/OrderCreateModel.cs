namespace GM_Warehouse.Components.Models.CreateModels;

public class OrderCreateModel 
{ 
    public Guid CustomerId { get; set; }
    public Guid SalesmanId { get; set; }
    public List<OrderItemCreateModel> OrderItems { get; set; } = [];

    public int Discount { get; set; }
}