using GM_Warehouse.Components.Models.DataModels;
using Microsoft.EntityFrameworkCore;

namespace GM_Warehouse.Data
{
    public class DataBaseContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataBaseContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<OrdersDataModel> Orders { get; set; }
        public DbSet<OrderItemDataModel> OrderItems { get; set; }
        public DbSet<CustomerDataModel> Customers { get; set; }
        public DbSet<ProductDataModel> Products { get; set; }
        public DbSet<SupplierDataModel> Suppliers { get; set; }
        public DbSet<SupplierOrdersDataModel> SupplierOrders { get; set; }
        public DbSet<SupplierOrderItemDataModel> SupplierOrderItems { get; set; }
        public DbSet<UserDataModel> Users { get; set; }
    }
}