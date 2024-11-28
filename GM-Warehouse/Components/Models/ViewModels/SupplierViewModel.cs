namespace GM_Warehouse.Components.Models.ViewModels;

public class SupplierViewModel 
{ 
    public Guid SupplierId { get; set; }

    public string Name { get; set; }

    public string Mail { get; set; }

    public string Address { get; set; }
    
    public UserViewModel Manager { get; set; }
    
    public override string ToString()
    {
        return "Name: " + Name + "\n Mail: " + Mail + "\n Address: " + Address;
    }
}