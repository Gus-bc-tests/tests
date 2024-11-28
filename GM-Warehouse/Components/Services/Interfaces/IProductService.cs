using GM_Warehouse.Components.Models.CreateModels;
using GM_Warehouse.Components.Models.DataModels;
using GM_Warehouse.Components.Models.ViewModels;

namespace GM_Warehouse.Components.Services.Interfaces;

public interface IProductService
{
    public Task<ProductDataModel> CreateProducts(ProductCreateModel model);
    public Task<IEnumerable<ProductViewModel>> GetAllProducts();
    public Task<ProductViewModel?> GetProductByIdAsync(Guid id);
    public Task UpdateReorderLevel(Guid ProductId, int ReorderLevel);
    
}