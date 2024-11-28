using AutoMapper;
using GM_Warehouse.Components.Models.CreateModels;
using GM_Warehouse.Components.Models.DataModels;
using GM_Warehouse.Components.Models.ViewModels;
using GM_Warehouse.Components.Services.Interfaces;
using GM_Warehouse.Data;
using Microsoft.EntityFrameworkCore;

namespace GM_Warehouse.Components.Services;

public class CustomerService : ICustomerService
{
    private readonly DataBaseContext _dbContext;
    private readonly IMapper _mapper;

    public CustomerService(DataBaseContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    // Method to add a new customer
    public async Task<CustomerDataModel> AddCustomerAsync(CustomerCreateModel customer)
    {
        // Map CustomerCreateModel to CustomerDataModel
        var newCustomer = _mapper.Map<CustomerDataModel>(customer);

        // Set additional properties if needed (e.g., CustomerId is generated automatically)
        newCustomer.CustomerId = Guid.NewGuid();  // Ensure that CustomerId is set

        // Add the new customer to the database
        await _dbContext.Customers.AddAsync(newCustomer);
        await _dbContext.SaveChangesAsync();

        return newCustomer;
    }

    // Method to get all customers
    public async Task<IEnumerable<CustomerViewModel>> GetAllCustomers()
    {
        List<CustomerViewModel> customersList = [];
        
        var _customers = await _dbContext.Customers.ToListAsync();

        foreach (var _customer in _customers)
        {
            var customerViewModel = _mapper.Map<CustomerViewModel>(_customer);
            customerViewModel.ContactPerson = _mapper.Map<UserViewModel>(await _dbContext.Users.FindAsync(_customer.ContactPersonId));
            customerViewModel.Manager = _mapper.Map<UserViewModel>(await _dbContext.Users.FindAsync(_customer.ManagerId));
            customersList.Add(customerViewModel);
        }
        return customersList.AsEnumerable();
    }

    // Method to delete all customers (use with caution)
    public void DeleteAllCustomers()
    {
        // Fetch all customers and delete them
        var customers = _dbContext.Customers.ToList();

        _dbContext.Customers.RemoveRange(customers);
        _dbContext.SaveChanges();
    }
}