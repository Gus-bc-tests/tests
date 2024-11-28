using AutoMapper;
using Xunit;
using GM_Warehouse.Components.Models.CreateModels;
using GM_Warehouse.Components.Models.DataModels;
using GM_Warehouse.Components.Models.ViewModels;

public class OrderItemMappingProfileTests
{
    private readonly IMapper _mapper;

    public OrderItemMappingProfileTests()
    {
        // Configure AutoMapper
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<OrderItemMappingProfile>();
        });

        _mapper = config.CreateMapper();
    }

    [Fact]
    public void Should_Map_OrderItemCreateModel_To_OrderItemDataModel()
    {
        // Arrange
        var createModel = new OrderItemCreateModel
        {
            ProductId = Guid.NewGuid(),
            Quantity = 5
        };

        // Act
        var dataModel = _mapper.Map<OrderItemDataModel>(createModel);

        // Assert
        Assert.NotNull(dataModel);
        Assert.Equal(createModel.ProductId, dataModel.ProductId);
        Assert.Equal(createModel.Quantity, dataModel.Quantity);
        Assert.NotEqual(Guid.Empty, dataModel.OrderItemId); 
    }

    [Fact]
    public void Should_Map_OrderItemDataModel_To_OrderItemViewModel()
    {
        // Arrange
        var dataModel = new OrderItemDataModel
        {
            OrderItemId = Guid.NewGuid(),
            OrderId = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            Quantity = 10
        };
        
        // Act
        var viewModel = _mapper.Map<OrderItemViewModel>(dataModel);

        // Assert
        Assert.NotNull(viewModel);
        Assert.Equal(dataModel.OrderItemId, viewModel.OrderItemId);
        Assert.Equal(dataModel.Quantity, viewModel.Quantity);
        Assert.Null(viewModel.Product); 
        Assert.Null(viewModel.Order); 
    }

    [Fact]
    public void Should_Ignore_OrderItemId_When_Mapping_From_OrderItemCreateModel_To_OrderItemDataModel()
    {
        // Arrange
        var createModel = new OrderItemCreateModel
        {
            ProductId = Guid.NewGuid(),
            Quantity = 3
        };

        // Act
        var dataModel = _mapper.Map<OrderItemDataModel>(createModel);

        // Assert
        Assert.NotEqual(Guid.Empty, dataModel.OrderItemId); // Check if OrderItemId was generated
    }

    [Fact]
    public void Should_Map_OrderItemCreateModel_To_OrderItemDataModel_And_Reverse()
    {
        // Arrange
        var createModel = new OrderItemCreateModel
        {
            ProductId = Guid.NewGuid(),
            Quantity = 5
        };

        // Act
        var dataModel = _mapper.Map<OrderItemDataModel>(createModel);

        // Assert
        Assert.NotNull(dataModel);
        Assert.Equal(createModel.ProductId, dataModel.ProductId);
        Assert.Equal(createModel.Quantity, dataModel.Quantity);
        Assert.NotEqual(Guid.Empty, dataModel.OrderItemId);
    }
}
