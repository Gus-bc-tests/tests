using GM_Warehouse.Components.Models.Enums;

namespace GM_Warehouse.Components.Models.ViewModels;

public class UserViewModel
{
    public Guid UserId { get; set; }

    public string Name { get; set; }

    public string Mail { get; set; }

    public string Phone { get; set; }

    public UserPrivileges Privileges { get; set; }
    
    public override string ToString()
    {
        return "Name: " + Name + "\n Mail: " + Mail;
    }
}