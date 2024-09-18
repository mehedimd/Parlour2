using AutoMapper;
using Domain.Entities.Parlour;
using Domain.Utility.Common;
using Domain.ViewModel.PrServiceInfo;
using Interface.Repository;
using Microsoft.EntityFrameworkCore;
using Persistence.ContextModel;
using Repository.Base;

namespace Repository
{
    public class PrServiceInfoRepository : BaseRepository<PrServiceInfo>, IPrServiceInfoRepository, IDisposable
    {
        #region config
        private ApplicationDbContext context => Db as ApplicationDbContext;
        private readonly IMapper _IMapper;

        public PrServiceInfoRepository(ApplicationDbContext db, IMapper mapper) : base(db)
        {
            Db = db;
            _IMapper = mapper;
        }
        #endregion

        #region Search

        public async Task<DataTablePagination<PrServiceInfoSearchVm, PrServiceInfoSearchVm>>
            SearchAsync(DataTablePagination<PrServiceInfoSearchVm, PrServiceInfoSearchVm> vm)
        {
            var searchResult = context.PrServiceInfos
                .Include(c => c.Category)
                .AsQueryable().Where(c => !c.IsDeleted);
            var model = vm.SearchModel;

            if (model == null) throw new Exception("Search Service Info not found");

            if (!string.IsNullOrEmpty(vm.Search.Value))
            {
                var value = vm.Search.Value.Trim().ToLower();
                searchResult = searchResult.Where(c => c.ServiceName.ToLower().Contains(value));
            }
            var totalRecords = await searchResult.CountAsync();
            if (totalRecords > 0)
            {
                vm.recordsTotal = totalRecords;
                vm.recordsFiltered = totalRecords;
                vm.draw = vm.LineDraw ?? 0;

                var data = await searchResult.OrderBy(c => c.ServiceName)
                                             .Skip(vm.Start)
                                             .Take(vm.Length)
                                             .ToListAsync();

                vm.data = _IMapper.Map<List<PrServiceInfoSearchVm>>(data);

                var sl = vm.Start;

                foreach (var searchDto in vm.data)
                {
                    var filterData = data.Where(c => c.Id == searchDto.Id).FirstOrDefault();

                    searchDto.SerialNo = ++sl;
                    searchDto.CategoryName = filterData?.Category.CategoryName;
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
