using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GM_Warehouse.Components.Models.DataModels;
public class SupplierDataModel 
{ 
    [Key]
    [Required]
    public Guid SupplierId { get; set; } = Guid.NewGuid();

    [Required]
    [ForeignKey(nameof(ManagerId))]
    public Guid ManagerId { get; set; }
    
    [Required, MaxLength(255)]
    public string Name { get; set; }

    [Required, MaxLength(255)]
    public string Mail { get; set; }

    [MaxLength(500)]
    public string Address { get; set; }
}