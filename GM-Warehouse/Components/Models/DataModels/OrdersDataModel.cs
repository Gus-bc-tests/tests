using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GM_Warehouse.Components.Models.Enums;

namespace GM_Warehouse.Components.Models.DataModels;

public class OrdersDataModel 
{ 
    [Key]
    public Guid OrderId { get; set; }
        
    [Required]
    [ForeignKey(nameof(CustomerId))]
    public Guid CustomerId { get; set; }
    
    [Required]
    [ForeignKey(nameof(SalesmanId))]
    public Guid SalesmanId { get; set; }

    public DateTime OrderDate { get; set; }

    [Required]
    public State Status { get; set; }

    public List<Guid> OrderItemDataModelIds { get; set; } = [];

    public decimal TotalPrice { get; set; }

    public int Discount { get; set; }
}