using System.ComponentModel.DataAnnotations;
using GM_Warehouse.Components.Models.Enums;

namespace GM_Warehouse.Components.Models.DataModels;

public class UserDataModel
{
    [Key]
    public Guid UserId { get; set; } = Guid.NewGuid();

    [Required, MaxLength(100)]
    public string Name { get; set; }

    [Required, MaxLength(100)]
    public string Mail { get; set; }

    [MaxLength(15)]
    public string Phone { get; set; }

    [Required]
    public UserPrivileges Privileges { get; set; }
    
    public override string ToString()
    {
        return "Person: " + Name + "\nMail: " + Mail;
    }
}