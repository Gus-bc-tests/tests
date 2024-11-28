using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GM_Warehouse.Components.Models.DataModels;
public class OrderItemDataModel
{
  
    [Key]
    [Required] 
    public Guid OrderItemId { get; set; } = Guid.NewGuid();
        
    [Required]
    [ForeignKey(nameof(OrderId))]
    public Guid OrderId { get; set; }

    [Required] 
        
    [ForeignKey(nameof(ProductId))]
    public Guid ProductId { get; set; }

    [Required] 
    public int Quantity { get; set; }

}