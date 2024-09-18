﻿using AutoMapper;
using Domain.Entities;
using Domain.ViewModel.EmpExperience;
using Interface.Repository;
using Interface.Services;
using Interface.UnitOfWork;
using Services.Base;

namespace Services
{
    public class EmpExperienceService : BaseService<EmpExperience>, IEmpExperienceService
    {
        #region Config

        private IEmpExperienceRepository Repository;
        private readonly IMapper _iMapper;
        private readonly IUnitOfWork _iUnitOfWork;

        public EmpExperienceService(IEmpExperienceRepository iRepository, IMapper iMapper, IUnitOfWork iUnitOfWork) : base(iRepository, iUnitOfWork)
        {
            Repository = iRepository;
            _iMapper = iMapper;
            _iUnitOfWork = iUnitOfWork;
        }

        #endregion

        #region Search

        public async Task<List<EmpExperienceVm>> GetEmpExperienceByEmpId(long empId)
        {
            if (empId <= 0) return new List<EmpExperienceVm>();
            var data = await Repository.GetAsync(c => c.EmployeeId == empId && !c.IsDeleted);
            var dataList = _iMapper.Map<List<EmpExperienceVm>>(data);
            return dataList;
        }

        #endregion
    }
}
