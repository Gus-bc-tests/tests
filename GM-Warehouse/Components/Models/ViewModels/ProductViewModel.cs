namespace GM_Warehouse.Components.Models.ViewModels;

public class ProductViewModel
{
    public Guid ProductId { get; set; }
    
    public SupplierViewModel Supplier { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
    
    public decimal Price { get; set; }
    
    public double Weight { get; set; }
    
    public int Length { get; set; }
    
    public int Width { get; set; }
    
    public int Height { get; set; }
    
    public int Quantity { get; set; }
    
    public DateTime LastOrdered { get; set; }    
        
    public int ReorderLevel { get; set; }
    
    public override string ToString()
    {
        return "Name: " + Name + "\n Weight: " + Weight + "\n Price: " + Price;
    }
}