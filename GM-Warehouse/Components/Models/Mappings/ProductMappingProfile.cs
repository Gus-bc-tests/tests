using AutoMapper;
using GM_Warehouse.Components.Models.CreateModels;
using GM_Warehouse.Components.Models.DataModels;
using GM_Warehouse.Components.Models.ViewModels;

namespace GM_Warehouse.Components.Mappings;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        // Map between ProductDataModel and ProductViewModel
        CreateMap<ProductDataModel, ProductViewModel>()
            .ForMember(dest => dest.Supplier, opt  => opt.Ignore());

        // Map between ProductCreateModel and ProductDataModel
        CreateMap<ProductCreateModel, ProductDataModel>()
            .ForMember(dest => dest.ProductId, opt => opt.Ignore()) // Ignore ProductId as it's generated
            .ForMember(dest => dest.Quantity, opt => opt.Ignore()) // Ignore Quantity, defaults to 0
            .ForMember(dest => dest.LastOrdered, opt => opt.Ignore()); // Ignore LastOrdered, defaults to DateTime.MinValue
    }
}