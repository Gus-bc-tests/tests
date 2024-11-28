using GM_Warehouse.Components.Models.ViewModels;

namespace GM_Warehouse.Components.Models.CreateModels;
public class SupplierCreateModel 
{ 
    public string Name { get; set; }

    public string Mail { get; set; }

    public string Address { get; set; }
    
    public Guid ManagerID { get; set; }
}