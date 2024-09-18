using AutoMapper;
using Domain.Entities;
using Domain.ViewModel.EmpPosting;
using Interface.Services;
using Interface.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Controllers.Base;
using DU = Domain.Utility;

namespace WebMVC.Controllers
{
    public class EmpPostingController : AppBaseController
    {

        #region Config

        private readonly IUnitOfWork _iUnitWork;
        private readonly IEmpPostingService _iService;
        private readonly IMapper _iMapper;

        public EmpPostingController(IUnitOfWork iUnitOfWork,
                                IEmpPostingService iService,
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
            var model = new EmpPostingVm();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] EmpPostingVm modelVm)
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

                var postingFile = modelVm.GetAppFileToUploadFolder();

                var model = _iMapper.Map<EmpPosting>(modelVm);
                model.ActionById = UserId;
                model.ActionDate = DU.Utility.GetBdDateTimeNow();
                model.DateFrom = (DateTime)(!string.IsNullOrEmpty(modelVm.DateFromStr) ? DU.Utility.ConvertStrToDate(modelVm.DateFromStr) : null);
                model.DateTo = !string.IsNullOrEmpty(modelVm.DateToStr) ? DU.Utility.ConvertStrToDate(modelVm.DateToStr) : null;

                var isAdded = await _iService.AddAsync(model);
                if (modelVm.IsAjaxPost)
                {
                    await DU.Utility.UploadFileToFolderAsync(postingFile);
                    return Ok(isAdded);
                }

                if (!isAdded)
                {
                    SaveFailedMsg();
                    return View("Create", modelVm);
                }

                await DU.Utility.UploadFileToFolderAsync(postingFile);
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

        #region GetEmployeePosting

        [HttpGet]
        public async Task<ActionResult> GetPostingByEmpId(long empId)
        {
            try
            {
                var references = await _iService.GetPostingByEmpId(empId);
                return Ok(references);
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
