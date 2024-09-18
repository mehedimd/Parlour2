using AutoMapper;
using Domain.Entities;
using Domain.ViewModel.EmpTraining;
using Interface.Services;
using Interface.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Controllers.Base;
using DU = Domain.Utility;

namespace WebMVC.Controllers
{
    public class EmpTrainingController : AppBaseController
    {
        #region Config

        private readonly IUnitOfWork _iUnitWork;
        private readonly IEmpTrainingService _iService;
        private readonly IMapper _iMapper;

        public EmpTrainingController(IUnitOfWork iUnitOfWork,
                                IEmpTrainingService iService,
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
            var model = new EmpTrainingVm();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] EmpTrainingVm modelVm)
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

                var model = _iMapper.Map<EmpTraining>(modelVm);

                model.DateFrom = !string.IsNullOrEmpty(modelVm.DateFromStr) ? DU.Utility.ConvertStrToDate(modelVm.DateFromStr) : null;
                model.DateTo = !string.IsNullOrEmpty(modelVm.DateToStr) ? DU.Utility.ConvertStrToDate(modelVm.DateToStr) : null;
                model.ActionById = UserId;
                model.ActionDate = DU.Utility.GetBdDateTimeNow();

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

                await DU.Utility.UploadFileToFolderAsync(photoFile);
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

        #region GetEmployeeTraining

        [HttpGet]
        public async Task<ActionResult> GetTrainingByEmpId(long empId)
        {
            try
            {
                var trainings = await _iService.GetTrainingByEmpId(empId);
                return Ok(trainings);
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
