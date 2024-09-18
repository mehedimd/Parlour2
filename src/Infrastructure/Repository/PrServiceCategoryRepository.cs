using AutoMapper;
using DocumentFormat.OpenXml.InkML;
using Domain.Entities.Parlour;
using Domain.Utility.Common;
using Domain.ViewModel.Department;
using Domain.ViewModel.PrServiceCategory;
using Interface.Repository;
using Microsoft.EntityFrameworkCore;
using Persistence.ContextModel;
using Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PrServiceCategoryRepository : BaseRepository<PrServiceCategory>, IPrServiceCategoryRepository, IDisposable
    {
        #region config
        private ApplicationDbContext context => Db as ApplicationDbContext;
        private readonly IMapper _IMapper;

        public PrServiceCategoryRepository(ApplicationDbContext db, IMapper mapper) : base(db)
        {
            Db = db;
            _IMapper = mapper;
        }
        #endregion

        #region Search

        public async Task<DataTablePagination<PrServiceCategorySearchVm, PrServiceCategorySearchVm>>
            SearchAsync(DataTablePagination<PrServiceCategorySearchVm, PrServiceCategorySearchVm> vm)
        {
            var searchResult = context.PrServiceCategories.AsQueryable().Where(c => !c.IsDeleted);
            var model = vm.SearchModel;

            if (model == null) throw new Exception("Search Service Category not found");

            if (!string.IsNullOrEmpty(vm.Search.Value))
            {
                var value = vm.Search.Value.Trim().ToLower();
                searchResult = searchResult.Where(c => c.CategoryName.ToLower().Contains(value));
            }
            var totalRecords = await searchResult.CountAsync();
            if (totalRecords > 0)
            {
                vm.recordsTotal = totalRecords;
                vm.recordsFiltered = totalRecords;
                vm.draw = vm.LineDraw ?? 0;

                var data = await searchResult.OrderBy(c => c.CategoryName)
                                             .Skip(vm.Start)
                                             .Take(vm.Length)
                                             .ToListAsync();

                vm.data = _IMapper.Map<List<PrServiceCategorySearchVm>>(data);

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
            context.Dispose();
        }
        #endregion
    }
}
