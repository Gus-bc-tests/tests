using AutoMapper;
using GM_Warehouse.Components.Models.CreateModels;
using GM_Warehouse.Components.Models.DataModels;
using GM_Warehouse.Components.Models.ViewModels;

public class SupplierOrderItemMappingProfile : Profile
{
    public SupplierOrderItemMappingProfile()
    {
        // Map SupplierOrderItemCreateModel to SupplierOrderItemDataModel
        CreateMap<SupplierOrderItemCreateModel, SupplierOrderItemDataModel>()
            .ForMember(dest => dest.SupplierOrderItemId, opt => opt.Ignore()); // Auto-generate ID

        // Map SupplierOrderItemDataModel to SupplierOrderItemViewModel
        CreateMap<SupplierOrderItemDataModel, SupplierOrderItemViewModel>()
            .ForMember(dest => dest.SupplierOrder, opt => opt.Ignore()) // Populate manually
            .ForMember(dest => dest.Product, opt => opt.Ignore()); // Populate manually
    }
}