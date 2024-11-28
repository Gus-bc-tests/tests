using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMWarehouse.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CompanyName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Address = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    ManagerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ContactPersonId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    OrderId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CustomerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SalesmanId = table.Column<Guid>(type: "TEXT", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderItemDataModelIds = table.Column<string>(type: "TEXT", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    Discount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SupplierId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Weight = table.Column<double>(type: "REAL", nullable: false),
                    Length = table.Column<int>(type: "INTEGER", nullable: false),
                    Width = table.Column<int>(type: "INTEGER", nullable: false),
                    Height = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    LastOrdered = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReorderLevel = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "SupplierOrderItems",
                columns: table => new
                {
                    SupplierOrderItemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SupplierOrderId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierOrderItems", x => x.SupplierOrderItemId);
                });

            migrationBuilder.CreateTable(
                name: "SupplierOrders",
                columns: table => new
                {
                    SupplierOrderId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SupplierId = table.Column<Guid>(type: "TEXT", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SupplierOrderItemDataModelIds = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierOrders", x => x.SupplierOrderId);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ManagerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Mail = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Address = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Mail = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                    Privileges = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });
            
            
            // Insert mock data into each table
            // Variables for IDs
            var customerId = Guid.Parse("1A2B3C4D-1234-5678-ABCD-12345678ABCD");
            var customerId2 = Guid.Parse("1A2B3C2D-1234-5678-ABCD-12345678ABCD");

            var supplier1Id = Guid.Parse("205E62B1-45B5-428D-BDC8-6FEBEE6934A7");
            var supplier2Id = Guid.Parse("8A7B6C5D-9876-5432-BDC8-123456789012");
            
            var product2Id = Guid.Parse("2B3C4D5E-5678-ABCD-1234-9876543210AB");
            var managerId2 = Guid.Parse("5C4D3E2F-2345-6789-ABCD-9876543210FE");
            
            var product1Id = Guid.Parse("1A1B1C1D-1234-5678-ABCD-123456789ABC"); // Fixed 'PQRS'
            var product3Id = Guid.Parse("3C4D5E6F-ABCD-1234-5678-6543210ABCDE"); // Fixed last segment to 12 digits
            var product4Id = Guid.Parse("4D5E6F7A-1234-5678-ABCD-ABCDEF123456"); // Replaced '7G' with '7A'
            var managerId1 = Guid.Parse("4B3C2D1E-1234-5678-ABCD-12345678ABCD"); // Fixed 'WXYZ' to 'ABCD'
            var managerId3 = Guid.Parse("6D5E4F3A-3456-789A-BCDE-0123456789AB"); // Replaced '3G' with '3A'
            var SalesmanId1 = Guid.Parse("7E6F5A4B-3537-89AB-CDEF-234567890ABC"); // Fixed '5G4H' to '5A4B'
            var SalesmanId2 = Guid.Parse("7E6F5A4B-3237-89AB-CDEF-234567890ABC"); // Fixed '5G4H' to '5A4B'
            var contactPersonId = Guid.Parse("7E6F5A4B-4567-89AB-CDEF-234567890ABC"); // Same fix as above
            var contactPersonId2 = Guid.Parse("7E2F5A4B-4567-89AB-CDEF-234567890ABC"); // Same fix as above
            
            
            // Insert Customers
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CompanyName", "Address", "ManagerId", "ContactPersonId" },
                values: new object[]
                {
                    customerId,
                    "Customer 1 Ltd.",
                    "123 Customer Lane, Cityville, Country",
                    managerId1,
                    contactPersonId
                });

            // Insert Customers
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CompanyName", "Address", "ManagerId", "ContactPersonId" },
                values: new object[]
                {
                    customerId2,
                    "Customer 2 Ltd.",
                    "321 Lane Customer, Cityville, Country",
                    managerId2,
                    contactPersonId2
                });
            
            // Insert Suppliers
            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "SupplierId", "ManagerId", "Name", "Mail", "Address" },
                values: new object[]
                {
                    supplier1Id,
                    managerId2,
                    "Default Supplier",
                    "default_supplier@example.com",
                    "123 Supplier Address, City, Country"
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "SupplierId", "ManagerId", "Name", "Mail", "Address" },
                values: new object[]
                {
                    supplier2Id,
                    managerId3,
                    "Secondary Supplier",
                    "secondary_supplier@example.com",
                    "456 Supplier Blvd, Town, Country"
                });

            // Insert Products
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "SupplierId", "Name", "Description", "Price", "Weight", "Length", "Width", "Height", "Quantity", "LastOrdered", "ReorderLevel" },
                values: new object[,]
                {
                    { product1Id, supplier1Id, "Product A", "Description for Product A", 49.99m, 2.5, 10, 15, 20, 100, DateTime.UtcNow, 10 },
                    { product2Id, supplier1Id, "Product B", "Description for Product B", 29.99m, 1.5, 8, 10, 12, 200, DateTime.UtcNow, 20 },
                    { product3Id, supplier2Id, "Product C", "Description for Product C", 19.99m, 1.0, 5, 7, 9, 300, DateTime.UtcNow, 15 },
                    { product4Id, supplier2Id, "Product D", "Description for Product D", 99.99m, 3.0, 12, 18, 25, 50, DateTime.UtcNow, 5 }
                });

            // Insert Users
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Name", "Mail", "Phone", "Privileges" },
                values: new object[,]
                {
                    { managerId1, "John Doe", "john.doe@example.com", "123-456-7890", 6 },
                    { managerId2, "Jane Smith", "jane.smith@example.com", "123-456-7891", 6 },
                    { managerId3, "Michael Brown", "michael.brown@example.com", "123-456-7892", 6 },
                    { contactPersonId, "David John", "David.davis@example.com", "123-456-7893", 7 },
                });
            
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Name", "Mail", "Phone", "Privileges" },
                values: new object[,]
                {
                    { SalesmanId1, "Emily Davis", "emily.davis@example.com", "123-456-7893", 5 },
                    { SalesmanId2, "David Jones", "Jones.David@example.com", "123-456-7893", 5 }
                });
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "SupplierOrderItems");

            migrationBuilder.DropTable(
                name: "SupplierOrders");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
