using AutoMapper;
using GM_Warehouse.Components.Models.CreateModels;
using GM_Warehouse.Components.Models.DataModels;
using GM_Warehouse.Components.Models.ViewModels;
using GM_Warehouse.Components.Services.Interfaces;
using GM_Warehouse.Data;
using Microsoft.EntityFrameworkCore;

namespace GM_Warehouse.Components.Services;

public class ProductService : IProductService
{
    private readonly DataBaseContext _dbContext;
    private readonly IMapper _mapper;

    public ProductService(DataBaseContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    // Create a new product
    public async Task<ProductDataModel> CreateProducts(ProductCreateModel model)
    {
        // Map CreateModel to DataModel
        var productDataModel = _mapper.Map<ProductDataModel>(model);

        // Add product to the database
        await _dbContext.Products.AddAsync(productDataModel);

        // Save changes to the database
        await _dbContext.SaveChangesAsync();

        return productDataModel;
    }

    // Get all products
    public async Task<IEnumerable<ProductViewModel>> GetAllProducts()
    {
        List<ProductViewModel> _productList = [];
        
        var _products = await _dbContext.Products.ToListAsync();

        foreach (var _product in _products)
        {
            var _productViewModel = _mapper.Map<ProductViewModel>(_product);
            _productViewModel.Supplier = _mapper.Map<SupplierViewModel>(await _dbContext.Suppliers.FindAsync(_product.SupplierId));
            _productList.Add(_productViewModel);
        }
        return _productList.AsEnumerable();
    }

    // Get product by ID
    public async Task<ProductViewModel> GetProductByIdAsync(Guid id)
    {
        // Find product by ID
        var product = await _dbContext.Products.FindAsync(id);

        if (product == null)
        {
            throw new KeyNotFoundException("Product not found.");
        }

        // Map the ProductDataModel to ProductViewModel
        return _mapper.Map<ProductViewModel>(product);
    }

    // Delete all products from the stock
    public void DeleteEntireStock()
    {
        // Remove all products from the database
        _dbContext.Products.RemoveRange(_dbContext.Products);

        // Save changes to the database
        _dbContext.SaveChanges();
    }
    
    // Update product quantity
    public async Task<ProductDataModel> UpdateProductQuantity(Guid productId, int newQuantity)
    {
        // Find the product by its ID
        var product = await _dbContext.Products.FindAsync(productId);

        if (product == null)
        {
            throw new KeyNotFoundException("Product not found.");
        }

        // Update the quantity of the product
        product.Quantity = newQuantity;
        //await _dbContext.Products.Update( where )
        
        // Save the changes to the database
        await _dbContext.SaveChangesAsync();

        return product;
    }
    
    // Update product reorder level
    public async Task UpdateReorderLevel(Guid productId, int newReorderLevel)
    {
        // Find the product by its ID
        var product = await _dbContext.Products.FindAsync(productId);

        if (product == null)
        {
            throw new KeyNotFoundException("Product not found.");
        }

        // Update the reorder level of the product
        product.ReorderLevel = newReorderLevel;

        // Save the changes to the database
        await _dbContext.SaveChangesAsync();
    }
}