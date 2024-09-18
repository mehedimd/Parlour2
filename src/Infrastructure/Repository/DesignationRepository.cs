﻿using AutoMapper;
using Domain.Entities.Admin;
using Domain.Utility.Common;
using Domain.ViewModel.Designation;
using Interface.Repository;
using Microsoft.EntityFrameworkCore;
using Persistence.ContextModel;
using Repository.Base;

namespace Repository;

public class DesignationRepository : BaseRepository<Designation>, IDesignationRepository, IDisposable
{
    #region Config
    private ApplicationDbContext Context => Db as ApplicationDbContext;
    private readonly IMapper _iMapper;

    public DesignationRepository(ApplicationDbContext db, IMapper iMapper) : base(db)
    {
        Db = db;
        _iMapper = iMapper;
    }

    #endregion

    #region Search

    public async Task<DataTablePagination<DesignationSearchVm, DesignationSearchVm>> SearchAsync(DataTablePagination<DesignationSearchVm, DesignationSearchVm> vm)
    {
        var searchResult = Context.Designations.AsQueryable().Where(c => !c.IsDeleted);
        var model = vm.SearchModel;

        if (model == null) throw new Exception("Search Designation not found");

        if (!string.IsNullOrEmpty(vm.Search.Value))
        {
            var value = vm.Search.Value.Trim().ToLower();
            searchResult = searchResult.Where(c => c.Name.ToLower().Contains(value));
        }
        var totalRecords = await searchResult.CountAsync();
        if (totalRecords > 0)
        {
            vm.recordsTotal = totalRecords;
            vm.recordsFiltered = totalRecords;
            vm.draw = vm.LineDraw ?? 0;

            var data = await searchResult.OrderBy(c => c.Code)
                                         .Skip(vm.Start)
                                         .Take(vm.Length)
                                         .ToListAsync();

            vm.data = _iMapper.Map<List<DesignationSearchVm>>(data);

            var sl = vm.Start;

            foreach (var searchDto in vm.data)
            {
                var filterData = data.Where(c => c.Id == searchDto.Id).FirstOrDefault();

                searchDto.SerialNo = ++sl;
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
