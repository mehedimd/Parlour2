using AutoMapper;
using Domain.Entities.Parlour;
using Domain.Enums.AppEnums;
using Domain.Utility.Common;
using Domain.ViewModel.PrCustomer;
using Interface.Repository;
using Interface.Services;
using Interface.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Utility.CachingUtility;
using WebMVC.Controllers.Base;

namespace WebMVC.Controllers;

public class PrCustomerController : AppBaseController
{
    #region Config

    private readonly IUnitOfWork _iUnitWork;
    private readonly IPrCustomerService _iService;
    private readonly DropdownService _dropdownService;
    private readonly IMapper _iMapper;
    private readonly IWebHostEnvironment _env;
    private readonly IHttpContextAccessor _iHttpContextAccessor;
    private readonly IAutoCodeRepository _iAutoCodeRepository;

    public PrCustomerController(IUnitOfWork iUnitOfWork,
                            IPrCustomerService iService,
                            DropdownService dropdownService,
                            IMapper iMapper,
                            IWebHostEnvironment iWebHostEnvironment,
                            IHttpContextAccessor iHttpContextAccessor,
                                    IAutoCodeRepository iAutoCodeRepository) : base(iUnitOfWork)
    {
        _iUnitWork = iUnitOfWork;
        _iService = iService;
        _dropdownService = dropdownService;
        _iMapper = iMapper;
        _env = iWebHostEnvironment;
        _iHttpContextAccessor = iHttpContextAccessor;
        _iAutoCodeRepository = iAutoCodeRepository;
    }

    #endregion

    #region JsonData

    [HttpGet]
    public async Task<ActionResult> GetCustomerByMobile(string mobile)
    {
        try
        {
            var customer = await _iService.GetCustomerByMobile(mobile);
            return Ok(customer);
        }
        catch (Exception ex)
        {
            return Ok(SetError(ex.Message));
        }
    }

    #endregion

    #region GetRegistrationNo

    public async Task<string> GetRegistrationNo()
    {
        var data = await _iAutoCodeRepository.GetMaxAutoCode(TableEnum.PrCustomers.ToString(), "RegistrationNo", prefix: $"C", howManyDigit: 6);
        return data;
    }

    #endregion

    #region Create

    [HttpGet]
    public  IActionResult Create()
    {
        var model = new PrCustomerVm();
        model.BranchListLookup =  _dropdownService.GetBranchSelectListItems();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(PrCustomerVm modelVm)
    {
        modelVm.BranchListLookup = _dropdownService.GetBranchSelectListItems();
        try
        {
            if (ModelState.IsValid)
            {
                var model = _iMapper.Map<PrCustomer>(modelVm);
                model.RegistrationNo = await GetRegistrationNo();
                model.RegistrationDate = DateTime.Now;
                model.ActionById = UserId;
                model.ActionDate = Domain.Utility.Utility.GetBdDateTimeNow();

                var isAdded = await _iService.AddAsync(model);
                if (!isAdded)
                {
                    SaveFailedMsg();
                    return View("Create", modelVm);
                }
                SaveSuccessMsg();
                return RedirectToAction("Search");
            }
        }
        catch (Exception ex)
        {
            ExceptionMsg(ex.Message);
            return View("Create", modelVm);
        }
        return RedirectToAction("Index");
    }


    #endregion

    #region Search
    [HttpGet]
    public IActionResult Search()
    {
        var modelVm = new PrCustomerSearchVm();
        return View(modelVm);
    }


    [HttpPost]
    [ProducesResponseType(201, Type = typeof(IEnumerable<JsonResult>))]
    public async Task<IActionResult>
            Search(DataTablePagination<PrCustomerSearchVm, PrCustomerSearchVm> searchVm = null)
    {
        if (searchVm == null) searchVm = new DataTablePagination<PrCustomerSearchVm, PrCustomerSearchVm>();
        if (searchVm?.SearchModel == null) searchVm.SearchModel = new PrCustomerSearchVm();
        var dataTable = await _iService.SearchAsync(searchVm);
        return dataTable == null ? NotFound() : Ok(dataTable);
    }

    #endregion

    #region Edit
    [HttpGet]
    public async Task<IActionResult> Edit(long id)
    {
        try
        {
            if (id <= 0)
            {
                return NotFoundMsg();
            }
            var data = await _iService.GetByIdAsync(id);
            if (data == null)
            {
                return NotFoundMsg();
            }
            var model = _iMapper.Map<PrCustomerVm>(data);
            model.BranchListLookup = _dropdownService.GetBranchSelectListItems();
            return View(model);

        }
        catch (Exception ex)
        {
            ExceptionMsg(ex.Message);
            return View("_404");
        }

    }
    [HttpPost]
    public async Task<IActionResult> Edit(PrCustomerVm modelVm)
    {
        modelVm.BranchListLookup = _dropdownService.GetBranchSelectListItems();

        try
        {
            if (modelVm.Id <= 0 || !ModelState.IsValid) return View("Edit", modelVm);
            var model = _iMapper.Map<PrCustomer>(modelVm);
            model.ActionById = UserId;
            model.ActionDate = Domain.Utility.Utility.GetBdDateTimeNow();
            model.UpdatedById = UserId;
            model.UpdateDate = Domain.Utility.Utility.GetBdDateTimeNow();

            var isUpdated = await _iService.UpdateAsync(model);
            if (!isUpdated)
            {
                UpdateFailedMsg();
                return View("Edit", modelVm);
            }
            UpdateSuccessMsg();
            return RedirectToAction("Search");
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
        return RedirectToAction("Search");
    }
    #endregion

    #region Details
    [HttpGet]
    public async Task<IActionResult> Details(long id)
    {
        try
        {
            if (id <= 0)
            {
                return NotFoundMsg();
            }
            var data = await _iService.GetByIdAsync(id);
            if (data == null)
            {
                return NotFoundMsg();
            }
            var model = _iMapper.Map<PrCustomerVm>(data);
            model.BranchListLookup = _dropdownService.GetBranchSelectListItems();
            return View(model);

        }
        catch (Exception ex)
        {
            ExceptionMsg(ex.Message);
            return View("_404");
        }
    }
    #endregion

}
