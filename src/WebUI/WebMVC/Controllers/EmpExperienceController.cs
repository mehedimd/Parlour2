using AutoMapper;
using Domain.Entities;
using Domain.ViewModel.EmpExperience;
using Interface.Services;
using Interface.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Controllers.Base;
using DU = Domain.Utility;

namespace WebMVC.Controllers
{
    public class EmpExperienceController : AppBaseController
    {
        #region Config

        private readonly IUnitOfWork _iUnitWork;
        private readonly IEmpExperienceService _iService;
        private readonly IMapper _iMapper;

        public EmpExperienceController(IUnitOfWork iUnitOfWork,
                                IEmpExperienceService iService,
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
            var model = new EmpExperienceVm();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] EmpExperienceVm modelVm)
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

                var experienceFile = modelVm.GetAppFileToUploadFolder();

                var model = _iMapper.Map<EmpExperience>(modelVm);
                model.ActionById = UserId;
                model.ActionDate = DU.Utility.GetBdDateTimeNow();
                model.DateFrom = !string.IsNullOrEmpty(modelVm.DateFromStr) ? DU.Utility.ConvertStrToDate(modelVm.DateFromStr) : null;
                model.DateTo = !string.IsNullOrEmpty(modelVm.DateToStr) ? DU.Utility.ConvertStrToDate(modelVm.DateToStr) : null;

                var isAdded = await _iService.AddAsync(model);

                if (modelVm.IsAjaxPost)
                {
                    await DU.Utility.UploadFileToFolderAsync(experienceFile);
                    return Ok(isAdded);
                }

                if (!isAdded)
                {
                    SaveFailedMsg();
                    return View("Create", modelVm);
                }

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

        #region GetEmployeeExperience

        [HttpGet]
        public async Task<ActionResult> GetExperienceByEmpId(long empId)
        {
            try
            {
                var educations = await _iService.GetEmpExperienceByEmpId(empId);
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
