using AutoMapper;
using Domain.Entities.Parlour;
using Domain.Utility.Common;
using Domain.ViewModel.PrCustomer;
using Interface.Repository;
using Microsoft.EntityFrameworkCore;
using Persistence.ContextModel;
using Repository.Base;

namespace Repository;

public class PrCustomerRepository : BaseRepository<PrCustomer>, IPrCustomerRepository, IDisposable
{
    #region Config
    private ApplicationDbContext Context => Db as ApplicationDbContext;
    private readonly IMapper _iMapper;

    public PrCustomerRepository(ApplicationDbContext db, IMapper iMapper) : base(db)
    {
        Db = db;
        _iMapper = iMapper;
    }
    #endregion

    #region Search

    public async Task<DataTablePagination<PrCustomerSearchVm, PrCustomerSearchVm>>
        SearchAsync(DataTablePagination<PrCustomerSearchVm, PrCustomerSearchVm> vm)
    {
        var searchResult = Context.PrCustomers
            .Include(b => b.Branch)
            .AsQueryable().Where(c => !c.IsDeleted);
        var model = vm.SearchModel;

        if (model == null) throw new Exception("Search Customer not found");

        if (!string.IsNullOrEmpty(vm.Search.Value))
        {
            var value = vm.Search.Value.Trim().ToLower();
            searchResult = searchResult.Where(c => c.CustomerName.ToLower().Contains(value) || c.Mobile.ToLower().Contains(value));
        }
        var totalRecords = await searchResult.CountAsync();
        if (totalRecords > 0)
        {
            vm.recordsTotal = totalRecords;
            vm.recordsFiltered = totalRecords;
            vm.draw = vm.LineDraw ?? 0;

            var data = await searchResult.OrderBy(c => c.ActionDate)
                                         .Skip(vm.Start)
                                         .Take(vm.Length)
                                         .ToListAsync();

            vm.data = _iMapper.Map<List<PrCustomerSearchVm>>(data);

            var sl = vm.Start;

            foreach (var searchDto in vm.data)
            {
                var filterData = data.Where(c => c.Id == searchDto.Id).FirstOrDefault();

                searchDto.SerialNo = ++sl;
                searchDto.BranchName = filterData?.Branch.BranchName;
            }
        }
        return vm;
    }

    #endregion

    #region Dispose

    public void Dispose()
    {
        Context.Dispose();
    }

    #endregion
}

