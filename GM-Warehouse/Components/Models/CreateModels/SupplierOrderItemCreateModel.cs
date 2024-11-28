using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GM_Warehouse.Components.Models.CreateModels;
public class SupplierOrderItemCreateModel
{
    [Key]
    public Guid SupplierOrderItemId { get; set; } = Guid.NewGuid();

    [Required]
    [ForeignKey(nameof(SupplierOrderId))]
    public Guid SupplierOrderId { get; set; }

    [Required]
    [ForeignKey(nameof(ProductId))]
    public Guid ProductId { get; set; }
    
    [Required]
    public int Quantity { get; set; }
}