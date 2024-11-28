using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GM_Warehouse.Components.Models.DataModels;

public class CustomerDataModel
{
    [Key]
    public Guid CustomerId { get; set; } = Guid.NewGuid();

    [Required, MaxLength(255)]
    public string CompanyName { get; set; } = "";

    [Required, MaxLength(500)] 
    public string Address { get; set; } = "";

    [Required]
    [ForeignKey(nameof(ManagerId))]
    public Guid ManagerId { get; set; }
        
    [Required]
    [ForeignKey(nameof(ContactPersonId))]
    public Guid ContactPersonId { get; set; }
}