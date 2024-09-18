using AutoMapper;
using Domain.Entities.Parlour;
using Domain.Utility.Common;
using Domain.ViewModel.PrServiceCategory;
using Interface.Services;
using Interface.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Controllers.Base;

namespace WebMVC.Controllers;

public class PrServiceCategoryController : AppBaseController
{
    #region config
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _iMapper;
    private readonly IPrServiceCategoryService _iService;
    public PrServiceCategoryController(IUnitOfWork unitOfWork, IMapper mapper, IPrServiceCategoryService prServiceCategoryService) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _iMapper = mapper;
        _iService = prServiceCategoryService;
    }
    #endregion

    #region Create
    [HttpGet]
    public IActionResult Create()
    {
        var model = new PrServiceCategoryVm();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(PrServiceCategoryVm modelVm)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                SaveFailedMsg("Information Is Not Correct");
                return View("Create", modelVm);
            }
            _iService.CurrentUserId = UserId;
            var model = _iMapper.Map<PrServiceCategory>(modelVm);
            model.ActionById = UserId;
            model.ActionDate = Domain.Utility.Utility.GetBdDateTimeNow();
            model.PhotoUrl = "demo url";

            var isAdded = await _iService.AddAsync(model);
            if (!isAdded)
            {
                SaveFailedMsg();
                return View("Create", modelVm);
            }

            SaveSuccessMsg();
            return RedirectToAction("Create");
        }
        catch (Exception ex)
        {
            ExceptionMsg(ex.Message);
            return View("Create", modelVm);
        }
    }
    #endregion

    #region Search
    [HttpGet]
    public IActionResult Search()
    {
        var vm = new PrServiceCategorySearchVm();
        return View("Create", vm);
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(IEnumerable<JsonResult>))]
    public async Task<IActionResult>
            Search(DataTablePagination<PrServiceCategorySearchVm, PrServiceCategorySearchVm> searchVm = null)
    {
        if (searchVm == null) searchVm = new DataTablePagination<PrServiceCategorySearchVm, PrServiceCategorySearchVm>();
        if (searchVm?.SearchModel == null) searchVm.SearchModel = new PrServiceCategorySearchVm();
        var dataTable = await _iService.SearchAsync(searchVm);
        return dataTable == null ? NotFound() : Ok(dataTable);
    }

    #endregion

    #region Edit
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            if (id > 0)
            {
                var data = await _iService.GetByIdAsync(id);
                if (data == null)
                {
                    return NotFoundMsg();
                }
                var model = _iMapper.Map<PrServiceCategoryVm>(data);
                return Json(model);

            }
            return View("Create");
        }
        catch (Exception ex)
        {
            ExceptionMsg(ex.Message);
            return View("_404");
        }

    }
    [HttpPost]
    public async Task<IActionResult> Edit(PrServiceCategoryVm modelVm)
    {
        try
        {
            if (modelVm.Id <= 0 || !ModelState.IsValid) return View("Create", modelVm);
            var model = _iMapper.Map<PrServiceCategory>(modelVm);
            model.ActionById = UserId;
            model.ActionDate = Domain.Utility.Utility.GetBdDateTimeNow();
            model.UpdatedById = UserId;
            model.UpdateDate = Domain.Utility.Utility.GetBdDateTimeNow();
            var isUpdated = await _iService.UpdateAsync(model);
            if (!isUpdated)
            {
                UpdateFailedMsg();
                return View("Create", modelVm);
            }
            UpdateSuccessMsg();
            return RedirectToAction("Create");
        }
        catch (Exception ex)
        {
            ExceptionMsg(ex.Message);
            return View(modelVm);
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
            DeleteFailedMsg();
            return NotFound();
        }
        data.IsDeleted = true;
        DeleteSuccessMsg();
        var isRemove = _iService.Update(data);
        return RedirectToAction("Create");
    }
    #endregion

    #region IsExists
    // name validation
    [HttpPost]
    public async Task<IActionResult> IsNameExists(string name, int id)
    {
        var result = await _iService.GetFirstOrDefaultAsync(c => c.CategoryName.Equals(name) && !c.IsDeleted);
        if (id > 0)
        {
            result = await _iService.GetFirstOrDefaultAsync(c => c.CategoryName.Equals(name) && c.Id != id && !c.IsDeleted);
        }
        if (result == null)
        {
            return Ok(false);
        }
        else
        {
            return Ok(true);
        }
    }
    // code validation
    [HttpPost]
    public async Task<IActionResult> IsCodeExists(string code, int id)
    {
        var res = await _iService.GetFirstOrDefaultAsync(c=>c.CategoryCode.Equals(code));
        var result = await _iService.GetFirstOrDefaultAsync(c => c.CategoryCode.Equals(code) && !c.IsDeleted);
        if (id > 0)
        {
            result = await _iService.GetFirstOrDefaultAsync(c => c.CategoryCode.Equals(code) && c.Id != id && !c.IsDeleted);
        }
        if (result == null)
        {
            return Ok(false);
        }
        else
        {
            return Ok(true);
        }
    }
    #endregion
}
