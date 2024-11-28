using AutoMapper;
using GM_Warehouse.Components.Mappings;
using GM_Warehouse.Components.Models.CreateModels;
using GM_Warehouse.Components.Models.DataModels;
using GM_Warehouse.Components.Models.Enums;
using GM_Warehouse.Components.Services;
using GM_Warehouse.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class OrderServiceTests
{
    private readonly DataBaseContext _dbContext;
    private readonly OrderService _orderService;

    public OrderServiceTests()
    {
        // Create a mock configuration with an in-memory SQLite connection string
        var configurationBuilder = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "ConnectionStrings:DefaultConnection", "DataSource=:memory:" }
            }!);

        var configuration = configurationBuilder.Build();
        
        // Initialize the DataBaseContext with the in-memory SQLite database
        _dbContext = new DataBaseContext(configuration);

        // Create and apply the schema
        _dbContext.Database.OpenConnection();
        _dbContext.Database.EnsureCreated();

        // Initialize AutoMapper
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CustomerMappingProfile>();
            cfg.AddProfile<OrderItemMappingProfile>();
            cfg.AddProfile<OrderMappingProfile>();
            cfg.AddProfile<ProductMappingProfile>();
            cfg.AddProfile<SupplierMappingProfile>();
            cfg.AddProfile<SupplierOrdersMappingProfile>();
            cfg.AddProfile<SupplierOrderItemMappingProfile>();
            cfg.AddProfile<UserMappingProfile>();
        });
        var mapper = config.CreateMapper();

        // Initialize the OrderService
        _orderService = new OrderService(_dbContext, mapper);

        // Seed data
        SeedMockData();
    }

        private void SeedMockData()
{
    var customerId = Guid.Parse("1A2B3C4D-1234-5678-ABCD-12345678ABCD");
    var customerId2 = Guid.Parse("1A2B3C2D-1234-5678-ABCD-12345678ABCD");

    var supplier1Id = Guid.Parse("205E62B1-45B5-428D-BDC8-6FEBEE6934A7");
    var supplier2Id = Guid.Parse("8A7B6C5D-9876-5432-BDC8-123456789012");

    var product1Id = Guid.Parse("1A1B1C1D-1234-5678-ABCD-123456789ABC");
    var product2Id = Guid.Parse("2B3C4D5E-5678-ABCD-1234-9876543210AB");
    var product3Id = Guid.Parse("3C4D5E6F-ABCD-1234-5678-6543210ABCDE");
    var product4Id = Guid.Parse("4D5E6F7A-1234-5678-ABCD-ABCDEF123456");

    var managerId1 = Guid.Parse("4B3C2D1E-1234-5678-ABCD-12345678ABCD");
    var managerId2 = Guid.Parse("5C4D3E2F-2345-6789-ABCD-9876543210FE");
    var managerId3 = Guid.Parse("6D5E4F3A-3456-789A-BCDE-0123456789AB");
    var salesmanId1 = Guid.Parse("7E6F5A4B-3537-89AB-CDEF-234567890ABC");
    var salesmanId2 = Guid.Parse("7E6F5A4B-3237-89AB-CDEF-234567890ABC");
    var contactPersonId = Guid.Parse("7E6F5A4B-4567-89AB-CDEF-234567890ABC");
    var contactPersonId2 = Guid.Parse("7E2F5A4B-4567-89AB-CDEF-234567890ABC");

    // Add Customers
    _dbContext.Customers.AddRange(new[]
    {
        new CustomerDataModel
        {
            CustomerId = customerId,
            CompanyName = "Customer 1 Ltd.",
            Address = "123 Customer Lane, Cityville, Country",
            ManagerId = managerId1,
            ContactPersonId = contactPersonId
        },
        new CustomerDataModel
        {
            CustomerId = customerId2,
            CompanyName = "Customer 2 Ltd.",
            Address = "321 Lane Customer, Cityville, Country",
            ManagerId = managerId2,
            ContactPersonId = contactPersonId2
        }
    });

    // Add Suppliers
    _dbContext.Suppliers.AddRange(new[]
    {
        new SupplierDataModel
        {
            SupplierId = supplier1Id,
            ManagerId = managerId2,
            Name = "Default Supplier",
            Mail = "default_supplier@example.com",
            Address = "123 Supplier Address, City, Country"
        },
        new SupplierDataModel
        {
            SupplierId = supplier2Id,
            ManagerId = managerId3,
            Name = "Secondary Supplier",
            Mail = "secondary_supplier@example.com",
            Address = "456 Supplier Blvd, Town, Country"
        }
    });

    // Add Products
    _dbContext.Products.AddRange(new[]
    {
        new ProductDataModel
        {
            ProductId = product1Id,
            SupplierId = supplier1Id,
            Name = "Product A",
            Description = "Description for Product A",
            Price = 49.99m,
            Weight = 2.5,
            Length = 10,
            Width = 15,
            Height = 20,
            Quantity = 100,
            LastOrdered = DateTime.UtcNow,
            ReorderLevel = 10
        },
        new ProductDataModel
        {
            ProductId = product2Id,
            SupplierId = supplier1Id,
            Name = "Product B",
            Description = "Description for Product B",
            Price = 29.99m,
            Weight = 1.5,
            Length = 8,
            Width = 10,
            Height = 12,
            Quantity = 200,
            LastOrdered = DateTime.UtcNow,
            ReorderLevel = 20
        },
        new ProductDataModel
        {
            ProductId = product3Id,
            SupplierId = supplier2Id,
            Name = "Product C",
            Description = "Description for Product C",
            Price = 19.99m,
            Weight = 1.0,
            Length = 5,
            Width = 7,
            Height = 9,
            Quantity = 300,
            LastOrdered = DateTime.UtcNow,
            ReorderLevel = 15
        },
        new ProductDataModel
        {
            ProductId = product4Id,
            SupplierId = supplier2Id,
            Name = "Product D",
            Description = "Description for Product D",
            Price = 99.99m,
            Weight = 3.0,
            Length = 12,
            Width = 18,
            Height = 25,
            Quantity = 50,
            LastOrdered = DateTime.UtcNow,
            ReorderLevel = 5
        }
    });

    // Add Users
    _dbContext.Users.AddRange(new[]
    {
        new UserDataModel
        {
            UserId = managerId1,
            Name = "John Doe",
            Mail = "john.doe@example.com",
            Phone = "123-456-7890",
            Privileges = UserPrivileges.Manager
        },
        new UserDataModel
        {
            UserId = managerId2,
            Name = "Jane Smith",
            Mail = "jane.smith@example.com",
            Phone = "123-456-7891",
            Privileges = UserPrivileges.Manager
        },
        new UserDataModel
        {
            UserId = managerId3,
            Name = "Michael Brown",
            Mail = "michael.brown@example.com",
            Phone = "123-456-7892",
            Privileges = UserPrivileges.Manager
        },
        new UserDataModel
        {
            UserId = contactPersonId,
            Name = "David John",
            Mail = "David.davis@example.com",
            Phone = "123-456-7893",
            Privileges = UserPrivileges.Contact
        },
        new UserDataModel
        {
            UserId = salesmanId1,
            Name = "Emily Davis",
            Mail = "emily.davis@example.com",
            Phone = "123-456-7893",
            Privileges = UserPrivileges.Salesperson
        },
        new UserDataModel
        {
            UserId = salesmanId2,
            Name = "David Jones",
            Mail = "Jones.David@example.com",
            Phone = "123-456-7893",
            Privileges = UserPrivileges.Salesperson
        }
    });

    _dbContext.SaveChanges();
}
    

    [Fact]
    public async Task CreateOrder_ShouldCreateOrderWithMappedValues()
    {
        // Use predefined IDs
        var product = _dbContext.Products.First();
        var customer = _dbContext.Customers.First();
        var salesman = _dbContext.Users.First(u => u.Privileges == UserPrivileges.Salesperson);

        // Arrange
        var orderCreateModel = new OrderCreateModel
        {
            Discount = 10,
            SalesmanId = salesman.UserId,
            CustomerId = customer.CustomerId,
            OrderItems = new List<OrderItemCreateModel>
            {
                new() { ProductId = product.ProductId, Quantity = 2 }
            }
        };

        // Act
        var result = await _orderService.CreateOrder(orderCreateModel);

        // Assert
        Assert.Equal(customer.CustomerId, result.CustomerId);
        Assert.Equal(10, result.Discount);
        Assert.Equal(salesman.UserId, result.SalesmanId);
        Assert.Equal((product.Price * 2) - 10, result.TotalPrice); // Price calculation
        Assert.Equal(State.Pending, result.Status);
        Assert.Single(result.OrderItemDataModelIds);
    }

    [Fact]
    public async Task GetAllOrders_ShouldReturnMappedOrderViewModels()
    {
        // Arrange
        var order = new OrdersDataModel
        {
            OrderId = Guid.NewGuid(),
            CustomerId = _dbContext.Customers.First().CustomerId,
            SalesmanId = _dbContext.Users.First(u => u.Privileges == UserPrivileges.Salesperson).UserId,
            OrderDate = DateTime.UtcNow,
            Status = State.Pending,
            TotalPrice = 100.50m,
            Discount = 10,
            OrderItemDataModelIds = new List<Guid>()
        };

        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _orderService.GetAllOrders();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(order.OrderId, result.First().OrderId);
    }

    [Fact]
    public async Task ChangeOrderStatusById_ShouldChangeStatusAndReturnTrue()
    {
        // Arrange
        Guid orderId = Guid.NewGuid();
        Guid customerId = _dbContext.Customers.First().CustomerId;
        Guid salesmanId = _dbContext.Users.First(u => u.Privileges == UserPrivileges.Salesperson).UserId;

        var order = new OrdersDataModel
        {
            OrderId = orderId,
            Status = State.Pending,
            CustomerId = customerId,
            SalesmanId = salesmanId,
            OrderDate = DateTime.UtcNow,
            TotalPrice = 100.0m,
            Discount = 10
        };

        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _orderService.ChangeOrderStatusById(State.DelivPending, order.OrderId);

        // Assert
        Assert.True(result);
        var updatedOrder = await _dbContext.Orders.FindAsync(order.OrderId);
        Assert.NotNull(updatedOrder);
        Assert.Equal(State.DelivPending, updatedOrder.Status);
    }

    [Fact]
    public async Task ChangeOrderStatusById_ShouldReturnFalseIfOrderNotFound()
    {
        // Arrange
        var nonExistentOrderId = Guid.NewGuid();

        // Act
        var result = await _orderService.ChangeOrderStatusById(State.Packing, nonExistentOrderId);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task GetOrderFromId_ShouldReturnMappedOrderViewModel()
    {
        // Arrange
        Guid orderId = Guid.NewGuid();
        Guid customerId = _dbContext.Customers.First().CustomerId;
        Guid salesmanId = _dbContext.Users.First(u => u.Privileges == UserPrivileges.Salesperson).UserId;

        var order = new OrdersDataModel
        {
            OrderId = orderId,
            Status = State.Pending,
            CustomerId = customerId,
            SalesmanId = salesmanId,
            OrderDate = DateTime.UtcNow,
            TotalPrice = 200.0m,
            Discount = 15
        };


        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _orderService.GetOrderFromId(order.OrderId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(orderId, result.OrderId);
        Assert.Equal(customerId, result.Customer.CustomerId);
        Assert.Equal(salesmanId, result.Salesman.UserId);
        Assert.Equal(200.0m, result.TotalPrice);
        Assert.Equal(15, result.Discount);
    }

    [Fact]
    public async Task GetOrderFromId_ShouldReturnNullIfOrderNotFound()
    {
        // Arrange
        var nonExistentOrderId = Guid.NewGuid();

        // Act
        var result = await _orderService.GetOrderFromId(nonExistentOrderId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetOrdersFromSalesman_ShouldReturnOrdersForGivenSalesman()
    {
        // Arrange
        var salesmanId = _dbContext.Users.First(u => u.Privileges == UserPrivileges.Salesperson).UserId;

        var orders = new List<OrdersDataModel>
        {
            new OrdersDataModel
            {
                OrderId = Guid.NewGuid(),
                CustomerId = _dbContext.Customers.First().CustomerId,
                SalesmanId = salesmanId,
                OrderDate = DateTime.UtcNow,
                Status = State.Pending,
                TotalPrice = 150.0m,
                Discount = 10
            },
            new OrdersDataModel
            {
                OrderId = Guid.NewGuid(),
                CustomerId = _dbContext.Customers.Skip(1).First().CustomerId,
                SalesmanId = salesmanId,
                OrderDate = DateTime.UtcNow,
                Status = State.DelivPending,
                TotalPrice = 200.0m,
                Discount = 5
            }
        };

        _dbContext.Orders.AddRange(orders);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _orderService.GetOrdersFromSalesman(salesmanId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.All(result, o => Assert.Equal(salesmanId, o.Salesman.UserId));
    }

    [Fact]
    public async Task GetOrdersFromSalesman_ShouldReturnEmptyIfNoOrdersForSalesman()
    {
        // Arrange
        var salesmanId = Guid.NewGuid(); // Use a non-existing salesman ID

        // Act
        var result = await _orderService.GetOrdersFromSalesman(salesmanId);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
