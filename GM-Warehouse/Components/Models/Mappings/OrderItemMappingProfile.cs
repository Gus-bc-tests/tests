using AutoMapper;
using GM_Warehouse.Components.Models.CreateModels;
using GM_Warehouse.Components.Models.DataModels;
using GM_Warehouse.Components.Models.ViewModels;

public class OrderItemMappingProfile : Profile
{
    public OrderItemMappingProfile()
    {
        // Map between OrderItemDataModel and OrderItemViewModel
        CreateMap<OrderItemDataModel, OrderItemViewModel>()
            .ForMember(dest => dest.Product, opt => opt.Ignore()) // Product will need to be mapped separately
            .ForMember(dest => dest.Order, opt => opt.Ignore()); // Order mapping requires additional context

        // Map between OrderItemCreateModel and OrderItemDataModel
        CreateMap<OrderItemCreateModel, OrderItemDataModel>()
            .ForMember(dest => dest.OrderItemId, opt => opt.Ignore()); // Ignore as it’s generated
    }
}