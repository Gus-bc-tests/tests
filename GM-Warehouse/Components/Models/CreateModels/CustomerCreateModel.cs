namespace GM_Warehouse.Components.Models.CreateModels;

public class CustomerCreateModel
{
    public string CompanyName { get; set; } = "";

    public string Address { get; set; } = "";

    public Guid ManagerId { get; set; }
        
    public Guid ContactPersonId { get; set; }
}