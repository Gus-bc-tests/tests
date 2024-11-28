using AutoMapper;
using GM_Warehouse.Components.Models.CreateModels;
using GM_Warehouse.Components.Models.DataModels;
using GM_Warehouse.Components.Models.ViewModels;

public class SupplierMappingProfile : Profile
{
    public SupplierMappingProfile()
    {
        // Map between SupplierCreateModel and SupplierDataModel
        CreateMap<SupplierCreateModel, SupplierDataModel>()
            .ForMember(dest => dest.SupplierId, opt => opt.Ignore()) // SupplierId is auto-generated
            .ForMember(dest => dest.ManagerId, opt => opt.MapFrom(src => src.ManagerID)); // Map ManagerId from UserId

        CreateMap<SupplierDataModel, SupplierCreateModel>()
            .ForMember(dest => dest.ManagerID, opt => opt.Ignore()); // Manager object can be fetched separately if needed

        // Map between SupplierDataModel and SupplierViewModel
        CreateMap<SupplierDataModel, SupplierViewModel>()
            .ForMember(dest => dest.Manager, opt => opt.Ignore()); // Manager object fetched separately

        CreateMap<SupplierViewModel, SupplierDataModel>()
            .ForMember(dest => dest.SupplierId, opt => opt.Ignore()) // SupplierId might be auto-generated or managed separately
            .ForMember(dest => dest.ManagerId, opt => opt.MapFrom(src => src.Manager.UserId));

        // Map between SupplierCreateModel and SupplierViewModel
        CreateMap<SupplierCreateModel, SupplierViewModel>();

        CreateMap<SupplierViewModel, SupplierCreateModel>();

        // Nested Mapping for Manager (User Models)
        CreateMap<UserDataModel, UserViewModel>();
        CreateMap<UserViewModel, UserDataModel>();
    }
}