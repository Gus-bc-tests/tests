using GM_Warehouse.Components.Models.CreateModels;
using GM_Warehouse.Components.Models.DataModels;
using GM_Warehouse.Components.Models.Enums;
using GM_Warehouse.Components.Models.ViewModels;

namespace GM_Warehouse.Components.Services.Interfaces;

public interface IUserService
{
    public Task<UserDataModel> AddUserAsync(UserCreateModel user);

    public Task<IEnumerable<UserViewModel>> GetAllUser();

    public void DeleteAllUsers();

    public Task<UserViewModel> GetUserByIdAsync(Guid id);

    public Task<IEnumerable<UserViewModel>> GetAllUserByPrivilegeAsync(UserPrivileges privilege);
}