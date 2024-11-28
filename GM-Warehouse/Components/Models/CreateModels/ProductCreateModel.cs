namespace GM_Warehouse.Components.Models.CreateModels;

public class ProductCreateModel
{
    public string Name { get; set; }
    
    public Guid SupplierId { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public double Weight { get; set; }
    
    public int Length { get; set; }
    
    public int Width { get; set; }
    
    public int Height { get; set; }

    public int ReorderLevel { get; set; }

}