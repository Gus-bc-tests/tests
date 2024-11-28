
using AutoMapper;
using GM_Warehouse.Components.Models.CreateModels;
using GM_Warehouse.Components.Models.DataModels;
using GM_Warehouse.Components.Models.ViewModels;

public class SupplierOrdersMappingProfile : Profile
{
    public SupplierOrdersMappingProfile()
    {
        // Map SupplierOrdersCreateModel to SupplierOrdersDataModel
        CreateMap<SupplierOrdersCreateModel, SupplierOrdersDataModel>()
            .ForMember(dest => dest.SupplierOrderId, opt => opt.Ignore()) // Auto-generate ID
            .ForMember(dest => dest.SupplierId, opt => opt.MapFrom(src => src.Supplier.SupplierId)) // Map SupplierId
            .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(_ => DateTime.UtcNow)) // Set current UTC time
            .ForMember(dest => dest.SupplierOrderItemDataModelIds, opt => opt.Ignore()); // Handle separately

        // Map SupplierOrdersDataModel to SupplierOrdersViewModel
        CreateMap<SupplierOrdersDataModel, SupplierOrdersViewModel>()
            .ForMember(dest => dest.Supplier, opt => opt.Ignore()) // Populate manually
            .ForMember(dest => dest.SupplierOrderItems, opt => opt.Ignore()); // Populate manually
    }
}