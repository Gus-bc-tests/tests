using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GM_Warehouse.Components.Models.DataModels;

public class ProductDataModel
{
    [Key]
    public Guid ProductId { get; set; } = Guid.NewGuid();
    
    [Required]
    [ForeignKey(nameof(SupplierId))]
    public Guid SupplierId { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public double Weight { get; set; }
    
    [Required] 
    public int Length { get; set; }
    
    [Required] 
    public int Width { get; set; }
    
    [Required] 
    public int Height { get; set; }
    
    [Required]
    public int Quantity { get; set; }
    
    public DateTime LastOrdered { get; set; } 
        
    [Required] 
    public int ReorderLevel { get; set; }
}