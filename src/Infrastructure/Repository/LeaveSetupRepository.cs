using AutoMapper;
using Domain.Entities;
using Domain.Utility.Common;
using Domain.ViewModel.LeaveSetup;
using Interface.Repository;
using Microsoft.EntityFrameworkCore;
using Persistence.ContextModel;
using Repository.Base;

namespace Repository
{
    public class LeaveSetupRepository : BaseRepository<LeaveSetup>, ILeaveSetupRepository, IDisposable
    {
        #region Config
        private ApplicationDbContext Context => Db as ApplicationDbContext;
        private readonly IMapper _iMapper;
        public LeaveSetupRepository(ApplicationDbContext db, IMapper iMapper) : base(db)
        {
            Db = db;
            _iMapper = iMapper;
        }
        #endregion

        #region Search

        public async Task<DataTablePagination<LeaveSetupSearchVm, LeaveSetupSearchVm>>
                                 SearchAsync(DataTablePagination<LeaveSetupSearchVm, LeaveSetupSearchVm> vm)
        {
            var searchResult = Context.LeaveSetups
                  .Include(s => s.LeaveType)
                  .AsQueryable().Where(c => !c.IsDeleted);

            var model = vm.SearchModel;

            if (model == null) throw new Exception("Search Leave Setup not found");

            if (!string.IsNullOrEmpty(vm.Search.Value))
            {
                var value = vm.Search.Value.Trim().ToLower();
                searchResult = searchResult.Where(c => c.LeaveType.TypeName.ToLower().Contains(value));
            }
            var totalRecords = await searchResult.CountAsync();
            if (totalRecords > 0)
            {
                vm.recordsTotal = totalRecords;
                vm.recordsFiltered = totalRecords;
                vm.draw = vm.LineDraw ?? 0;

                var data = await searchResult.OrderBy(c => c.LeaveType.TypeName)
                                             .Skip(vm.Start)
                                             .Take(vm.Length)
                                             .ToListAsync();

                vm.data = _iMapper.Map<List<LeaveSetupSearchVm>>(data);
                var sl = vm.Start;

                foreach (var searchDto in vm.data)
                {
                    var filterData = data.Where(c => c.Id == searchDto.Id).FirstOrDefault();

                    searchDto.SerialNo = ++sl;
                    searchDto.LeaveTypeName = filterData?.LeaveType?.TypeName;
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
}
