using AutoMapper;
using GM_Warehouse.Components.Models.CreateModels;
using GM_Warehouse.Components.Models.DataModels;
using GM_Warehouse.Components.Models.ViewModels;

public class CustomerMappingProfile : Profile
{
    public CustomerMappingProfile()
    {
        // Map between User models
        CreateMap<UserDataModel, UserViewModel>().ReverseMap();
        CreateMap<UserCreateModel, UserDataModel>();

        // Map between Customer models
        CreateMap<CustomerCreateModel, CustomerDataModel>()
            .ForMember(dest => dest.CustomerId, opt => opt.Ignore()); // Ignore as it’s generated in the data model

        CreateMap<CustomerDataModel, CustomerViewModel>()
            .ForMember(dest => dest.Manager, opt => opt.Ignore())
            .ForMember(dest => dest.ContactPerson, opt => opt.Ignore());
    }
}