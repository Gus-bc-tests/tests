using AutoMapper;
using GM_Warehouse.Components.Models.CreateModels;
using GM_Warehouse.Components.Models.DataModels;
using GM_Warehouse.Components.Models.Enums;
using GM_Warehouse.Components.Models.ViewModels;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        // Mapping from OrderCreateModel to OrdersDataModel
        CreateMap<OrderCreateModel, OrdersDataModel>()
            .ForMember(dest => dest.OrderId, opt => opt.Ignore()) // Assuming OrderId is auto-generated
            .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => DateTime.UtcNow)) // Set order date to current time
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => State.Pending)) // Default status
            .ForMember(dest => dest.TotalPrice, opt => opt.Ignore()) // Calculate later
            .ForMember(dest => dest.OrderItemDataModelIds, opt => opt.Ignore()); // Handle separately if needed

        // Mapping from OrdersDataModel to OrderViewModel
        CreateMap<OrdersDataModel, OrderViewModel>()
            .ForMember(dest => dest.Customer, opt => opt.Ignore()) // Map separately if UserViewModel is needed
            .ForMember(dest => dest.Salesman, opt => opt.Ignore()) // Map separately if UserViewModel is needed
            .ForMember(dest => dest.OrderItems, opt => opt.Ignore()); // Handle complex mapping for order items

        // Additional mappings for complex types
        CreateMap<OrderItemCreateModel, OrderItemDataModel>();

        CreateMap<OrderItemDataModel, OrderItemViewModel>();
    }
}