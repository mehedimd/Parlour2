using AutoMapper;
using Domain.Entities;
using Domain.ViewModel.EmpTraining;
using Interface.Repository;
using Interface.Services;
using Interface.UnitOfWork;
using Services.Base;
using DU = Domain.Utility;

namespace Services
{
    public class EmpTrainingService : BaseService<EmpTraining>, IEmpTrainingService
    {
        #region Config

        private IEmpTrainingRepository Repository;
        private readonly IMapper _iMapper;
        private readonly IUnitOfWork _iUnitOfWork;

        public EmpTrainingService(IEmpTrainingRepository iRepository, IMapper iMapper, IUnitOfWork iUnitOfWork) : base(iRepository, iUnitOfWork)
        {
            Repository = iRepository;
            _iMapper = iMapper;
            _iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region GetTrainingByEmpId

        public async Task<List<EmpTrainingVm>> GetTrainingByEmpId(long empId)
        {
            if (empId <= 0) return new List<EmpTrainingVm>();
            var data = await Repository.GetAsync(c => c.EmployeeId == empId && !c.IsDeleted);
            var dataList = _iMapper.Map<List<EmpTrainingVm>>(data);

            dataList.ForEach(c => c.DateFromStr = DU.Utility.ConvertDateTimeToStr((DateTime)c.DateFrom));
            dataList.ForEach(c => c.DateToStr = DU.Utility.ConvertDateTimeToStr((DateTime)c.DateTo));

            return dataList;
        }

        #endregion
    }
}
