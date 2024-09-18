﻿using AutoMapper;
using Domain.Entities;
using Domain.ViewModel.EmpEducation;
using Domain.ViewModel.EmpJournal;
using Interface.Services;
using Interface.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Controllers.Base;
using DU = Domain.Utility;

namespace WebMVC.Controllers
{
    public class EmpJournalController : AppBaseController
    {
        #region Config

        private readonly IUnitOfWork _iUnitWork;
        private readonly IEmpJournalService _iService;
        private readonly IMapper _iMapper;

        public EmpJournalController(IUnitOfWork iUnitOfWork,
                                IEmpJournalService iService,
                                IMapper iMapper) : base(iUnitOfWork)
        {
            _iUnitWork = iUnitOfWork;
            _iService = iService;
            _iMapper = iMapper;
        }

        #endregion

        #region Create

        [HttpGet]
        public ActionResult Create()
        {
            var model = new EmpJournalVm();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] EmpJournalVm modelVm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    if (modelVm.IsAjaxPost)
                    {
                        return Ok(false);
                    }

                    SaveFailedMsg("Information Is Not Correct");
                    return View("Create", modelVm);
                };

                var photoFile = modelVm.GetAppFileToUploadFolder();

                var model = _iMapper.Map<EmpJournal>(modelVm);
                model.ActionById = UserId;
                model.ActionDate = DU.Utility.GetBdDateTimeNow();
                model.SubmitDate = DU.Utility.GetBdDateTimeNow();

                var isAdded = await _iService.AddAsync(model);
                if (modelVm.IsAjaxPost)
                {
                    await DU.Utility.UploadFileToFolderAsync(photoFile);
                    return Ok(isAdded);
                }
                if (!isAdded)
                {
                    SaveFailedMsg();
                    return View("Create", modelVm);
                }

                //await DU.Utility.UploadFileToFolderAsync(photoFile);
                SaveSuccessMsg();

                return RedirectToAction("Create");
            }
            catch (Exception e)
            {
                ExceptionMsg(e.Message);
                return View("Create", modelVm);
            }
        }

        #endregion

        #region GetEmployeeJournal

        [HttpGet]
        public async Task<ActionResult> GetEmpJournalByEmpId(long empId)
        {
            try
            {
                var educations = await _iService.GetEmpJournalByEmpId(empId);
                return Ok(educations);
            }
            catch (Exception ex)
            {
                return Ok(SetError(ex.Message));
            }
        }

        #endregion

        #region Delete

        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            var data = await _iService.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            data.IsDeleted = true;
            var isRemove = _iService.Update(data);
            return Ok(isRemove);
        }

        #endregion
    }
}
