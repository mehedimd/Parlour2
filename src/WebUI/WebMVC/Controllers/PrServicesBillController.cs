using AutoMapper;
using Domain.Dto;
using Domain.Utility.Common;
using Domain.ViewModel.PrServicesBill;
using Interface.Services;
using Interface.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utility.CachingUtility;
using Utility.Export;
using WebMVC.Controllers.Base;
using WebMVC.Models.IdentityModels;

namespace WebMVC.Controllers;

public class PrServicesBillController : AppBaseController
{
    #region Config

    private readonly IUnitOfWork _iUnitWork;
    private readonly IPrServicesBillService _iService;
    private readonly DropdownService _dropdownService;
    private readonly IMapper _iMapper;
    private readonly IWebHostEnvironment _env;
    private readonly IHttpContextAccessor _iHttpContextAccessor;

    public PrServicesBillController(IUnitOfWork iUnitOfWork,
                            IPrServicesBillService iService,
                            DropdownService dropdownService,
                            IMapper iMapper,
                            IWebHostEnvironment iWebHostEnvironment,
                            IHttpContextAccessor iHttpContextAccessor) : base(iUnitOfWork)
    {
        _iUnitWork = iUnitOfWork;
        _iService = iService;
        _dropdownService = dropdownService;
        _iMapper = iMapper;
        _env = iWebHostEnvironment;
        _iHttpContextAccessor = iHttpContextAccessor;
    }

    #endregion

    #region Create

    [HttpGet]
    [Authorize(Permissions.PrServicesBills.Create)]
    public async Task<IActionResult> Create()
    {
        var model = new PrServicesBillVm();
        model.ServiceNo = await _iService.GetInvoiceNo();
        model.ServiceDateStr = DateTime.Now.ToString("dd/MM/yyyy");
        model.ShiftLookUp = _dropdownService.GetServiceShiftSelectListItems();
        model.PrCategoryLookUp = _dropdownService.GetServiceCategorySelectListItems();
        model.PayModeLookUp = _dropdownService.GetPayModeSelectListItems();
        return View(model);
    }

    [HttpGet]
    [Authorize(Permissions.PrServicesBills.Create)]
    public async Task<IActionResult> Appointment()
    {
        var model = new PrServicesBillVm();
        model.ServiceNo = await _iService.GetInvoiceNo();
        model.ServiceDateStr = DateTime.Now.ToString("dd/MM/yyyy");
        model.ShiftLookUp = _dropdownService.GetServiceShiftSelectListItems();
        model.PrCategoryLookUp = _dropdownService.GetServiceCategorySelectListItems();
        model.PayModeLookUp = _dropdownService.GetPayModeSelectListItems();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(PrServicesBillVm modelVm)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                SaveFailedMsg("Information Is Not Correct");
                return View("Create", modelVm);
            }
            _iService.CurrentUserId = UserId;
            var (isAdded, billId) = await _iService.BillGenerate(modelVm);

            if (!isAdded)
            {
                SaveFailedMsg();
                return View("Create", modelVm);
            }

            SaveSuccessMsg();
            return RedirectToAction("Details", new { id = billId });
        }
        catch (Exception e)
        {
            ExceptionMsg(e.Message);
            return View("Create", modelVm);
        }
    }

    #endregion

    #region Search

    [HttpGet]
    [Authorize(Permissions.PrServicesBills.ListView)]
    public IActionResult Search()
    {
        var vm = new PrServicesBillSearchVm();
        vm.BillStatusLookUp = _dropdownService.GetBillStatusSelectListItems();
        vm.CustomerLookUp = _dropdownService.GetCustomerSelectListItems();
        return View(vm);
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(IEnumerable<JsonResult>))]
    public async Task<IActionResult>
        Search(DataTablePagination<PrServicesBillSearchVm, PrServicesBillSearchVm> searchVm = null)
    {
        if (searchVm == null) searchVm = new DataTablePagination<PrServicesBillSearchVm, PrServicesBillSearchVm>();
        if (searchVm?.SearchModel == null) searchVm.SearchModel = new PrServicesBillSearchVm();
        var dataTable = await _iService.SearchAsync(searchVm);
        return dataTable == null ? NotFound() : Ok(dataTable);
    }

    #endregion

    #region Details

    [Authorize(Permissions.PrServicesBills.Details)]
    public async Task<IActionResult> Details(long id)
    {
        var model = await _iService.GetBillInfoById(id);
        model.PaidDateStr = DateTime.Now.ToString("dd/MM/yyyy");
        model.PrCategoryLookUp = _dropdownService.GetServiceCategorySelectListItems();
        model.PrServiceLookUp = _dropdownService.GetDefaultSelectListItem();
        model.PayModeLookUp = _dropdownService.GetPayModeSelectListItems();
        return View(model);
    }

    #endregion

    #region JsonData

    [HttpGet]
    public async Task<ActionResult> GetServiceByCategoryId(long? categoryId)
    {
        try
        {
            var services = await _iService.GetServiceByCategoryId(categoryId);
            return Ok(services);
        }
        catch (Exception ex)
        {
            return Ok(SetError(ex.Message));
        }
    }

    #endregion

    #region ServiceBillPrint
    [Authorize(Permissions.PrServicesBills.Details)]
    public async Task<ActionResult> ServiceBillPrint(long id)
    {
        string html = await _iService.GetServiceBillByIdAsyncHtml(id);

        ExportToPDF export = new ExportToPDF(_iHttpContextAccessor);
        ExportDataTitle reportTitle = new ExportDataTitle();
        reportTitle.Header = PrintInfo.CompanyName;
        reportTitle.AddressOne = PrintInfo.CompanyAddress;
        reportTitle.ReportTitle = "";
        reportTitle.IsSignature = false;
        reportTitle.SignatureList = new List<string>();
        reportTitle.SignatureList.Add("Manager");
        reportTitle.SignatureList.Add("");
        reportTitle.SignatureList.Add("Client");

        string reportName = "Food order details_" + DateTime.Today.ToString("dd_mm_yyy");

        return File(export.ExportReceiptContentToPdf(html, reportName, reportTitle: reportTitle, isLandScape: false, withFooter: true), "application/pdf");
    }

    #endregion

    #region BillServiceAdd

    [HttpPost]
    public async Task<IActionResult> BillServiceAdd(SavePrServiceBillDetailDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "Validation failed",
                    errors = ModelState.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                )
                });
            };

            _iService.CurrentUserId = UserId;
            var isAdded = await _iService.BillServiceAddAsync(dto);
            return Ok(isAdded);
        }
        catch (Exception e)
        {
            return BadRequest(new
            {
                message = "Entry Failed..!!",
                errors = e.Message
            });
        }
    }

    #endregion

    #region BillPayment

    [Authorize(Permissions.PrServicesBills.Details)]
    [HttpPost]
    public async Task<IActionResult> BillPaymentEntry(ServicePaymentDto modelVm)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "Validation failed",
                    errors = ModelState.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                )
                });
            };

            _iService.CurrentUserId = UserId;
            var isAdded = await _iService.PaymentEntry(modelVm);
            return Ok(isAdded);
        }
        catch (Exception e)
        {
            return BadRequest(new
            {
                message = "Entry Failed..!!",
                errors = e.Message
            });
        }
    }

    #endregion
}
