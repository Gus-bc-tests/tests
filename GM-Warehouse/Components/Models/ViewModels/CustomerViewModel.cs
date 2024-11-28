namespace GM_Warehouse.Components.Models.ViewModels;

public class CustomerViewModel
{
    public Guid CustomerId { get; set; } 

    public string CompanyName { get; set; } = "";

    public string Address { get; set; } = "";

    public UserViewModel Manager { get; set; }
        
    public UserViewModel ContactPerson { get; set; }
        
    public override string ToString()
    {
        return "CustomerId: " + CustomerId
                              + "\nCompanyName: " + CompanyName
                              + "\nAddress: " + Address
                              + "\nManagerId: " + Manager
                              + "\nContactPersonId: " + ContactPerson;
    }
}