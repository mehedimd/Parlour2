﻿using AutoMapper;
using Domain.Entities;
using Domain.Utility.Common;
using Domain.ViewModel.LeaveType;
using Interface.Repository;
using Interface.Services;
using Interface.UnitOfWork;
using Services.Base;

namespace Services
{
    public class LeaveTypeService : BaseService<LeaveType>, ILeaveTypeService
    {
        #region Config
        private readonly ILeaveTypeRepository Repository;
        private readonly IMapper _iMapper;
        private readonly IUnitOfWork _iUnitOfWork;

        public LeaveTypeService(ILeaveTypeRepository iRepository, IMapper iMapper, IUnitOfWork iUnitOfWork) : base(iRepository, iUnitOfWork)
        {
            Repository = iRepository;
            _iMapper = iMapper;
            _iUnitOfWork = iUnitOfWork;
        }
        #endregion

        #region Search

        public async Task<DataTablePagination<LeaveTypeSearchVm, LeaveTypeSearchVm>> SearchAsync(DataTablePagination<LeaveTypeSearchVm, LeaveTypeSearchVm> model)
        {
            var dataList = await Repository.SearchAsync(model);
            return dataList;
        }

        #endregion
    }
}
