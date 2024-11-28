using AutoMapper;
using GM_Warehouse.Components.Models.CreateModels;
using GM_Warehouse.Components.Models.DataModels;
using GM_Warehouse.Components.Models.ViewModels;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<UserDataModel, UserViewModel>().ReverseMap();
        CreateMap<UserCreateModel, UserDataModel>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore()); // Ignore UserId as it's generated in UserDataModel
    }
}