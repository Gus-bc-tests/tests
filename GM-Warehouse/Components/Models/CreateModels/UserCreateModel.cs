using System.ComponentModel.DataAnnotations;
using GM_Warehouse.Components.Models.Enums;

namespace GM_Warehouse.Components.Models.CreateModels;

public class UserCreateModel
{
    [Required, MaxLength(100)]
    public string Name { get; set; }

    [Required, EmailAddress, MaxLength(100)]
    public string Mail { get; set; }

    [MaxLength(15)]
    public string Phone { get; set; }

    [Required]
    public UserPrivileges Privileges { get; set; }
}

