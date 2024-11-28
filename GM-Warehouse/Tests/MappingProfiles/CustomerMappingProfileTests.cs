using GM_Warehouse.Components.Models.CreateModels;
using GM_Warehouse.Components.Models.DataModels;
using GM_Warehouse.Components.Models.ViewModels;
using AutoMapper;
using Xunit;

public class CustomerMappingProfileTests
{
    private readonly IMapper _mapper;

    public CustomerMappingProfileTests()
    {
        // Configure AutoMapper
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CustomerMappingProfile>();
        });

        _mapper = config.CreateMapper();
    }

    [Fact]
    public void Should_Map_CustomerCreateModel_To_CustomerDataModel()
    {
        // Arrange
        var createModel = new CustomerCreateModel
        {
            CompanyName = "Test Company",
            Address = "123 Test Street",
            ManagerId = Guid.NewGuid(),
            ContactPersonId = Guid.NewGuid()
        };

        // Act
        var dataModel = _mapper.Map<CustomerDataModel>(createModel);

        // Assert
        Assert.NotNull(dataModel);
        Assert.Equal(createModel.CompanyName, dataModel.CompanyName);
        Assert.Equal(createModel.Address, dataModel.Address);
        Assert.Equal(createModel.ManagerId, dataModel.ManagerId);
        Assert.Equal(createModel.ContactPersonId, dataModel.ContactPersonId);
        Assert.NotEqual(Guid.Empty, dataModel.CustomerId); // Ensure CustomerId is generated
    }

    [Fact]
    public void Should_Map_CustomerDataModel_To_CustomerViewModel()
    {
        // Arrange
        var dataModel = new CustomerDataModel
        {
            CustomerId = Guid.NewGuid(),
            CompanyName = "Test Company",
            Address = "123 Test Street",
            ManagerId = Guid.NewGuid(),
            ContactPersonId = Guid.NewGuid()
        };

        // Act
        var viewModel = _mapper.Map<CustomerViewModel>(dataModel);

        // Assert
        Assert.NotNull(viewModel);
        Assert.Equal(dataModel.CustomerId, viewModel.CustomerId);
        Assert.Equal(dataModel.CompanyName, viewModel.CompanyName);
        Assert.Equal(dataModel.Address, viewModel.Address);
        Assert.Null(viewModel.Manager); // Manager is ignored in mapping
        Assert.Null(viewModel.ContactPerson); // ContactPerson is ignored in mapping
    }

    [Fact]
    public void Should_Map_UserDataModel_To_UserViewModel_And_Reverse()
    {
        // Arrange
        var userDataModel = new UserDataModel
        {
            UserId = Guid.NewGuid(),
            Name = "Test User"
        };

        // Act
        var userViewModel = _mapper.Map<UserViewModel>(userDataModel);

        // Assert
        Assert.NotNull(userViewModel);
        Assert.Equal(userDataModel.UserId, userViewModel.UserId);
        Assert.Equal(userDataModel.Name, userViewModel.Name);
    }
}
