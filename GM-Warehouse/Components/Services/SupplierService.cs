using AutoMapper;
using GM_Warehouse.Components.Models.CreateModels;
using GM_Warehouse.Components.Models.DataModels;
using GM_Warehouse.Components.Models.ViewModels;
using GM_Warehouse.Components.Services.Interfaces;
using GM_Warehouse.Data;
using Microsoft.EntityFrameworkCore;

namespace GM_Warehouse.Components.Services;
public class SupplierService : ISupplierService
{
    private readonly DataBaseContext _dbContext;
    private readonly IMapper _mapper;

    public SupplierService(DataBaseContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<IEnumerable<SupplierViewModel>> GetAllSuppliers()
    {
        List<SupplierViewModel> _supplierList = [];
        
        var _suppliers = await _dbContext.Suppliers.ToListAsync();

        foreach (var _supplier in _suppliers)
        {
            var _viewModel = _mapper.Map<SupplierViewModel>(_supplier);
            _supplierList.Add(_viewModel);
        }
        return _supplierList.AsEnumerable();
    }

    // Get Supplier by ID
    public async Task<SupplierViewModel?> GetSupplierByIdAsync(Guid id)
    {
        // Find the supplier by ID
        var supplier = await _dbContext.Suppliers.FindAsync(id);

        if (supplier == null)
        {
            return null;
        }

        // Map the DataModel to ViewModel
        return _mapper.Map<SupplierViewModel>(supplier);
    }
}