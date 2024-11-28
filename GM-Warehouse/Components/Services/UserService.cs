using AutoMapper;
using GM_Warehouse.Components.Models.CreateModels;
using GM_Warehouse.Components.Models.DataModels;
using GM_Warehouse.Components.Models.Enums;
using GM_Warehouse.Components.Models.ViewModels;
using GM_Warehouse.Components.Services.Interfaces;
using GM_Warehouse.Data;
using Microsoft.EntityFrameworkCore;

namespace GM_Warehouse.Components.Services;

public class UserService : IUserService
{
    private readonly DataBaseContext _dbContext;
    private readonly IMapper _mapper;

    public UserService(DataBaseContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    // Add a new user
    public async Task<UserDataModel> AddUserAsync(UserCreateModel user)
    {
        // Map CreateModel to DataModel
        var userDataModel = _mapper.Map<UserDataModel>(user);
        
        // Add user to database
        await _dbContext.Users.AddAsync(userDataModel);
        
        // Save changes
        await _dbContext.SaveChangesAsync();

        return userDataModel;
    }

    // Get all users by privilege
    public async Task<IEnumerable<UserViewModel>> GetAllUserByPrivilegeAsync(UserPrivileges privilege)
    {
        var users = await _dbContext.Users
            .Where(user => user.Privileges == privilege) // Filter users by privilege
            .ToListAsync();

        return _mapper.Map<List<UserViewModel>>(users);
    }
    
    // Get a list of all users
    public async Task<IEnumerable<UserViewModel>> GetAllUser()
    {
        var users = await _dbContext.Users.ToListAsync();
        
        // Map DataModel list to ViewModel list
        return _mapper.Map<List<UserViewModel>>(users);
    }

    // Delete all users
    public void DeleteAllUsers()
    {
        _dbContext.Users.RemoveRange(_dbContext.Users);
        _dbContext.SaveChanges();
    }

    // Get user by ID
    public async Task<UserViewModel> GetUserByIdAsync(Guid id)
    {
        var user = await _dbContext.Users.FindAsync(id);

        if (user == null)
        {
            throw new KeyNotFoundException("User not found.");
        }

        // Map DataModel to ViewModel
        return _mapper.Map<UserViewModel>(user);
    }
}