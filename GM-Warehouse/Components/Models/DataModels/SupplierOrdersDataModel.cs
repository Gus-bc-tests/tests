using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GM_Warehouse.Components.Models.Enums;

namespace GM_Warehouse.Components.Models.DataModels;

public class SupplierOrdersDataModel
{
    [Key]
    public Guid SupplierOrderId { get; set; } = Guid.NewGuid();

    [Required]
    [ForeignKey(nameof(SupplierId))]
    public Guid SupplierId { get; set; }

    [Required]
    public DateTime OrderDate { get; set; }
    
    [Required]
    public List<Guid> SupplierOrderItemDataModelIds { get; set; } = [];

    [Required]
    public State Status { get; set; }
}