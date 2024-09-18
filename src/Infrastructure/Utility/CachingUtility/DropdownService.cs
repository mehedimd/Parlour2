using Domain.Entities.Identity;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using Persistence.ContextModel;
using Domain.Utility;
using Domain.Entities;
using Domain.Enums.AppEnums;
using Domain.Entities.Admin;
using Domain.Entities.Parlour;

namespace Utility.CachingUtility;

public class DropdownService
{
    #region CONFIG

    private readonly ApplicationDbContext _db;
    private readonly CacheStoreService _cacheStoreService;
    public DropdownService(ApplicationDbContext db, CacheStoreService cacheStoreService)
    {
        _db = db;
        _cacheStoreService = cacheStoreService;
    }

    #endregion

    /* Default */
    #region DefaultSelectListItem

    public List<SelectListItem> GetDefaultSelectListItem(bool isDefaultSelectAdd = true)
    {
        var items = new List<SelectListItem>();
        if (isDefaultSelectAdd) items.Add(new SelectListItem { Value = "", Text = "---Select---" });
        return items;
    }

    #endregion

    /* A */
    #region ApplicationUsers

    public IEnumerable<SelectListItem> GetUserSelectListItems(bool isDefaultSelectAdd = true)
    {
        var items = GetDefaultSelectListItem(isDefaultSelectAdd);
        var dataList = CacheStore.Get<List<ApplicationUser>>(CacheEnum.UserList.ToString()) ?? _cacheStoreService.AddOrUpdate<ApplicationUser>(CacheEnum.UserList.ToString());
        items.AddRange(dataList.Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.FullName }));
        return items;
    }
    #endregion

    #region RoleSelectListItems

    public IEnumerable<SelectListItem> GetRoleSelectListItems(bool isDefaultSelectAdd = true)
    {
        var items = GetDefaultSelectListItem(isDefaultSelectAdd);
        var dataList = _cacheStoreService.GetSession<ApplicationRole>(CacheEnum.RoleList.ToString());
        dataList = dataList.Where(c => c.Id != RoleEnum.Administrator.ToInt64()).ToList();
        items.AddRange(dataList.Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.Name }));
        return items;
    }

    #endregion

    #region BloodGroupSelectListItems

    public IEnumerable<SelectListItem> GetBloodGroupSelectListItems(bool isDefaultSelectAdd = true)
    {
        var items = new List<SelectListItem>()
        {
            new SelectListItem { Value = "", Text = "---Select---" },
            new SelectListItem {Value="O+", Text = "O+ (ve)" },
            new SelectListItem {Value="O-", Text = "O- (ve)"},
            new SelectListItem {Value="A+", Text = "A+ (ve)"},
            new SelectListItem {Value="A-", Text = "A- (ve)"},
            new SelectListItem {Value="B+", Text = "B+ (ve)"},
            new SelectListItem {Value="B-", Text = "B- (ve)"},
            new SelectListItem {Value="AB+", Text = "AB+ (ve)"},
            new SelectListItem {Value="AB-", Text = "AB- (ve)"},
        };
        return items;
    }

    #endregion

    /* D */
    #region DepartmentSelectListItems

    public IEnumerable<SelectListItem> GetDepartmentSelectListItems(bool isDefaultSelectAdd = true)
    {
        var items = GetDefaultSelectListItem(isDefaultSelectAdd);
        var dataList = CacheStore.Get<List<Department>>(CacheEnum.DepartmentList.ToString()) ?? _cacheStoreService.AddOrUpdate<Department>(CacheEnum.DepartmentList.ToString());
        items.AddRange(dataList.Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.Name }));
        return items;
    }

    #endregion

    #region DesignationSelectListItems

    public IEnumerable<SelectListItem> GetDesignationSelectListItems(bool isDefaultSelectAdd = true)
    {
        var items = GetDefaultSelectListItem(isDefaultSelectAdd);
        var dataList = CacheStore.Get<List<Designation>>(CacheEnum.DesignationList.ToString()) ?? _cacheStoreService.AddOrUpdate<Designation>(CacheEnum.DesignationList.ToString());
        items.AddRange(dataList.Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.Name }));
        return items;
    }

    #endregion

    #region DiscountSelectListItems

    public IEnumerable<SelectListItem> GetDiscountSelectListItems(bool isDefaultSelectAdd = true)
    {
        var items = GetDefaultSelectListItem(isDefaultSelectAdd);
        items = new List<SelectListItem>()
            {
                new SelectListItem { Text = "---Select---", Value = "",  },
                new SelectListItem { Text = "Percent", Value = "P" },
                new SelectListItem { Text = "Amount", Value = "A" },
            };
        return items;
    }

    #endregion

    #region EmpLeaveTypeByGender

    public dynamic GetEmpLeaveTypeByGender(string gender)
    {
        var dataList = CacheStore.Get<List<LeaveType>>(CacheEnum.EmpLeaveType.ToString()) ?? _cacheStoreService.AddOrUpdate<LeaveType>(CacheEnum.EmpLeaveType.ToString());
        dataList = !string.IsNullOrEmpty(gender) ? dataList.Where(c => c.Gender == gender || c.Gender == "A").ToList() : dataList;
        var dynamicData = dataList.Select(c => new { c.Id, Name = c.TypeName });
        return dynamicData;
    }

    #endregion

    #region EmpLeaveTypeSelectListItems

    public IEnumerable<SelectListItem> GetEmpLeaveTypeSelectListItems(bool isDefaultSelectAdd = true)
    {
        var items = GetDefaultSelectListItem(isDefaultSelectAdd);
        var dataList = CacheStore.Get<List<LeaveType>>(CacheEnum.EmpLeaveType.ToString()) ?? _cacheStoreService.AddOrUpdate<LeaveType>(CacheEnum.EmpLeaveType.ToString());
        items.AddRange(dataList.Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.TypeName }));
        return items;
    }

    #endregion

    #region EmployeeActionTypeSelectListItems

    public IEnumerable<SelectListItem> GetEmployeeActionTypeSelectListItems(bool isDefaultSelectAdd = true)
    {
        var items = new List<SelectListItem>();
        items.AddRange(new List<SelectListItem>
        {
            new SelectListItem { Text = EmpDisciplinaryEnum.SowCause.GetDescription(), Value = EmpDisciplinaryEnum.SowCause.ToInt32ToString() },
            new SelectListItem { Text = EmpDisciplinaryEnum.Warning.GetDescription(), Value = EmpDisciplinaryEnum.Warning.ToInt32ToString() },
            new SelectListItem { Text = EmpDisciplinaryEnum.Suspend.GetDescription(), Value = EmpDisciplinaryEnum.Suspend.ToInt32ToString() },
            new SelectListItem { Text = EmpDisciplinaryEnum.Others.GetDescription(), Value = EmpDisciplinaryEnum.Others.ToInt32ToString() },

        });

        return items;
    }

    #endregion

    #region EmployeeLeaveApplicationStatus

    public IEnumerable<SelectListItem> EmployeeLeaveApplicationStatus(bool isDefaultSelectAdd = true)
    {
        var items = new List<SelectListItem>()
        {
            new SelectListItem { Value = "", Text = "---Select---" },
            new SelectListItem {Value="0", Text = "Pending" },
            new SelectListItem {Value="1", Text = "Approve"},
            new SelectListItem {Value="2", Text = "Reject"},
            new SelectListItem {Value="3", Text = "Cancel"},
        };
        return items;
    }

    #endregion

    #region EmployeeSelectListItems

    public IEnumerable<SelectListItem> GetEmployeeSelectListItems(bool isDefaultSelectAdd = true)
    {
        var items = GetDefaultSelectListItem(isDefaultSelectAdd);
        var dataList = CacheStore.Get<List<Employee>>(CacheEnum.EmployeeList.ToString()) ?? _cacheStoreService.AddOrUpdate<Employee>(CacheEnum.EmployeeList.ToString());
        items.AddRange(dataList.Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = $"{c.Code}_{c.Name}" }));
        return items;
    }

    #endregion

    #region EmployeeStatusSelectListItems

    public IEnumerable<SelectListItem> GetEmployeeStatusSelectListItems(bool isDefaultSelectAdd = true)
    {
        var items = new List<SelectListItem>();
        items.AddRange(new List<SelectListItem>
        {
            new SelectListItem { Text = EmployeeStatusEnum.Permanent.GetDescription(), Value = EmployeeStatusEnum.Permanent.ToInt32ToString() },
            new SelectListItem { Text = EmployeeStatusEnum.Contractual.GetDescription(), Value = EmployeeStatusEnum.Contractual.ToInt32ToString() },
            new SelectListItem { Text = EmployeeStatusEnum.Adhoc.GetDescription(), Value = EmployeeStatusEnum.Adhoc.ToInt32ToString() },
            new SelectListItem { Text = EmployeeStatusEnum.Guest.GetDescription(), Value = EmployeeStatusEnum.Guest.ToInt32ToString() },
        });

        return items;
    }

    #endregion

    #region EmpRefTypeSelectListItems

    public IEnumerable<SelectListItem> GetEmpRefTypeSelectListItems(bool isDefaultSelectAdd = true)
    {
        var items = new List<SelectListItem>();
        items.AddRange(new List<SelectListItem>
        {
            new SelectListItem { Text = EmpRefTypeEnum.REFERENCE.GetDescription(), Value = EmpRefTypeEnum.REFERENCE.ToInt32ToString() },
            new SelectListItem { Text = EmpRefTypeEnum.NOMINEE.GetDescription(), Value = EmpRefTypeEnum.NOMINEE.ToInt32ToString() },
            new SelectListItem { Text = EmpRefTypeEnum.EMERGENCY_CONTACT.GetDescription(), Value = EmpRefTypeEnum.EMERGENCY_CONTACT.ToInt32ToString() },
        });

        return items;
    }

    #endregion

    #region EmployeeStatus

    public IEnumerable<SelectListItem> GetEmployeeIsEnableSelectListItems(bool isDefaultSelectAdd = true)
    {
        var items = GetDefaultSelectListItem(isDefaultSelectAdd);
        //var items = new List<SelectListItem>();
        items.AddRange(new List<SelectListItem>
        {
            new SelectListItem { Text = EmployeeIsEnableEnum.Disable.GetDescription(), Value = EmployeeIsEnableEnum.Disable.ToInt32ToString() },
            new SelectListItem { Text = EmployeeIsEnableEnum.Current.GetDescription(), Value = EmployeeIsEnableEnum.Current.ToInt32ToString() },
        });

        return items;
    }

    #endregion

    /* G */
    #region GenderSelectListItems

    public IEnumerable<SelectListItem> GetGenderSelectListItems(bool isDefaultSelectAdd = true)
    {
        var items = GetDefaultSelectListItem(isDefaultSelectAdd);
        items = new List<SelectListItem>()
            {
                new SelectListItem { Text = "---Select---", Value = "",  },
                new SelectListItem { Text = "Male", Value = "M" },
                new SelectListItem { Text = "Female", Value = "F" },
            };
        return items;
    }

    #endregion

    #region GenderSelectListItemsForLeaveTypes

    public IEnumerable<SelectListItem> GetGenderSelectListItemsForLeaveTypes(bool isDefaultSelectAdd = true)
    {
        var items = new List<SelectListItem>()
            {
                new SelectListItem { Value = "", Text = "---Select---" },
                new SelectListItem {Value="F", Text = "Female" },
                new SelectListItem {Value="A", Text = "All"}
            };
        return items;
    }

    #endregion

    /* H */
    #region HolidaySelectListItems

    public IEnumerable<SelectListItem> GetHolidaySelectListItems(bool isDefaultSelectAdd = true)
    {
        var items = GetDefaultSelectListItem(isDefaultSelectAdd);
        items = new List<SelectListItem>()
            {
                new SelectListItem { Text = "---Select---", Value = "",  },
                new SelectListItem { Text = "FULL DAY", Value = "F" },
                new SelectListItem { Text = "HALF DAY", Value = "H" },
            };
        return items;
    }

    #endregion

    /* L */

    #region LeaveLimitTypeSelectListItems

    public IEnumerable<SelectListItem> GetLeaveLimitTypeSelectListItems(bool isDefaultSelectAdd = true)
    {
        var items = GetDefaultSelectListItem(isDefaultSelectAdd);
        items = new List<SelectListItem>()
            {
                new SelectListItem { Text = "---Select---", Value = "",  },
                new SelectListItem { Text = "Year", Value = "Y" },
                new SelectListItem { Text = "Job Life", Value = "L" },
            };
        return items;
    }

    #endregion

    /* M */

    #region MaritalStatusSelectListItems

    public IEnumerable<SelectListItem> GetMaritalStatusSelectListItems(bool isDefaultSelectAdd = true)
    {
        var items = GetDefaultSelectListItem(isDefaultSelectAdd);
        items = new List<SelectListItem>()
            {
                new SelectListItem { Text = "---Select---", Value = "",  },
                new SelectListItem { Text = "Unmarried", Value = "U" },
                new SelectListItem { Text = "Married", Value = "M" },
            };
        return items;
    }

    #endregion

    #region MonthSelectListItems

    public IEnumerable<SelectListItem> GetMonthSelectListItems(bool isDefaultSelectAdd = true)
    {
        //var items = new List<SelectListItem>();
        var items = GetDefaultSelectListItem(isDefaultSelectAdd);
        items.AddRange(new List<SelectListItem>
            {
                new SelectListItem { Text = MonthEnum.January.GetDescription(), Value = MonthEnum.January.ToInt32ToString() },
                new SelectListItem { Text = MonthEnum.February.GetDescription(), Value = MonthEnum.February.ToInt32ToString() },
                new SelectListItem { Text = MonthEnum.March.GetDescription(), Value = MonthEnum.March.ToInt32ToString() },
                new SelectListItem { Text = MonthEnum.April.GetDescription(), Value = MonthEnum.April.ToInt32ToString() },
                new SelectListItem { Text = MonthEnum.May.GetDescription(), Value = MonthEnum.May.ToInt32ToString() },
                new SelectListItem { Text = MonthEnum.June.GetDescription(), Value = MonthEnum.June.ToInt32ToString() },
                new SelectListItem { Text = MonthEnum.July.GetDescription(), Value = MonthEnum.July.ToInt32ToString() },
                new SelectListItem { Text = MonthEnum.August.GetDescription(), Value = MonthEnum.August.ToInt32ToString() },
                new SelectListItem { Text = MonthEnum.September.GetDescription(), Value = MonthEnum.September.ToInt32ToString() },
                new SelectListItem { Text = MonthEnum.October.GetDescription(), Value = MonthEnum.October.ToInt32ToString() },
                new SelectListItem { Text = MonthEnum.November.GetDescription(), Value = MonthEnum.November.ToInt32ToString() },
                new SelectListItem { Text = MonthEnum.December.GetDescription(), Value = MonthEnum.December.ToInt32ToString() }
            });

        return items;
    }

    #endregion

    #region SalaryTypeSelectListItems

    public IEnumerable<SelectListItem> GetSalaryTypeSelectListItems(bool isDefaultSelectAdd = true)
    {
        var items = new List<SelectListItem>();
        items.AddRange(new List<SelectListItem>
            {
                new SelectListItem { Text = SalaryTypeEnum.Monthly.GetDescription(), Value = SalaryTypeEnum.Monthly.ToInt32ToString() },
                new SelectListItem { Text = SalaryTypeEnum.ClassWise.GetDescription(), Value = SalaryTypeEnum.ClassWise.ToInt32ToString() },
            });

        return items;
    }

    #endregion

    #region YearSelectListItems

    public IEnumerable<SelectListItem> GetYearSelectListItems(bool isDefaultSelectAdd = true)
    {
        var items = new List<SelectListItem>();
        int startYear = 2000;
        var yearData = Enumerable.Range(startYear, DateTime.Now.Year - startYear + 5);

        foreach (var item in yearData)
        {
            items.Add(new SelectListItem { Text = item.ToString(), Value = item.ToString() });
        }
        items = items.OrderByDescending(c => c.Value).ToList();
        return items;
    }

    #endregion

    #region TypeSelectListItems

    public IEnumerable<SelectListItem> GetTypeSelectListItems(bool isDefaultSelectAdd = true)
    {
        var items = GetDefaultSelectListItem(isDefaultSelectAdd);
        items = new List<SelectListItem>()
            {
                new SelectListItem { Text = "---Select---", Value = ""},
                new SelectListItem { Text = "Mandatory", Value = "M" },
                new SelectListItem { Text = "Optional", Value = "O" },
            };
        return items;
    }

    #endregion

    #region ReligionSelectListItems

    public IEnumerable<SelectListItem> GetReligionSelectListItems(bool isDefaultSelectAdd = true)
    {
        var items = GetDefaultSelectListItem(isDefaultSelectAdd);
        items.AddRange(new List<SelectListItem>
            {
                new SelectListItem { Text = ReligionEnum.Islam.GetDescription(), Value = ReligionEnum.Islam.ToInt32ToString() },
                new SelectListItem { Text = ReligionEnum.Hindu.GetDescription(), Value = ReligionEnum.Hindu.ToInt32ToString() },
                new SelectListItem { Text = ReligionEnum.Christian.GetDescription(), Value = ReligionEnum.Christian.ToInt32ToString() },
                new SelectListItem { Text = ReligionEnum.Buddist.GetDescription(), Value = ReligionEnum.Buddist.ToInt32ToString() },
                new SelectListItem { Text = ReligionEnum.Other.GetDescription(), Value = ReligionEnum.Other.ToInt32ToString() },
            });
        return items;
    }

    #endregion

    #region PostingTypeSelectListItems

    public IEnumerable<SelectListItem> GetPostingTypeSelectListItems(bool isDefaultSelectAdd = true)
    {
        var items = new List<SelectListItem>()
            {
                new SelectListItem { Value = "", Text = "---Select---" },
                new SelectListItem {Value="R", Text = "REGULAR" },
                new SelectListItem {Value="S", Text = "OSD"},
                new SelectListItem {Value="P", Text = "PROMOTION"},
                new SelectListItem {Value="I", Text = "INCREMENT"},
                new SelectListItem {Value="B", Text = "PROMOTION & INCREMENT"},
                new SelectListItem {Value="O", Text = "OTHERS"}
            };
        return items;
    }

    #endregion

    /* S */
    #region ServiceShiftListItems
    public IEnumerable<SelectListItem> GetServiceShiftSelectListItems(bool isDefaultSelectAdd = true)
    {
        var items = GetDefaultSelectListItem(isDefaultSelectAdd);
        var dataList = CacheStore.Get<List<PrShift>>(CacheEnum.PrShiftList.ToString()) ?? _cacheStoreService.AddOrUpdate<PrShift>(CacheEnum.PrShiftList.ToString());
        items.AddRange(dataList.Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.ShiftName }));
        return items;
    }
    #endregion

    #region PayModeListItems
    public IEnumerable<SelectListItem> GetPayModeSelectListItems(bool isDefaultSelectAdd = true)
    {
        var items = new List<SelectListItem>()
            {
                new SelectListItem { Value = "", Text = "---Select---" },
                new SelectListItem {Value="Cash", Text = "Cash" },
                new SelectListItem {Value="Bank", Text = "Bank"},
                new SelectListItem {Value="Bkash", Text = "Bkash"},
                new SelectListItem {Value="Card", Text = "Card"}
            };
        return items;
    }
    #endregion

    #region ServiceCategoryListItems
    public IEnumerable<SelectListItem> GetServiceCategorySelectListItems(bool isDefaultSelectAdd = true)
    {
        var items = GetDefaultSelectListItem(isDefaultSelectAdd);
        var dataList = CacheStore.Get<List<PrServiceCategory>>(CacheEnum.PrServiceCategoryList.ToString())
            ?? _cacheStoreService.AddOrUpdate<PrServiceCategory>(CacheEnum.PrServiceCategoryList.ToString());
        var filteredList = dataList.Where(c => c.IsDeleted != true);
        items.AddRange(filteredList.Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.CategoryName }));
        return items;
    }
    #endregion

    #region BillSelectListItems

    public IEnumerable<SelectListItem> GetBillStatusSelectListItems(bool isDefaultSelectAdd = true)
    {
        var items = GetDefaultSelectListItem(isDefaultSelectAdd);
        items.AddRange(new List<SelectListItem>
            {
                new SelectListItem { Text = BillStatusEnum.Pending.GetDescription(), Value = BillStatusEnum.Pending.ToInt32ToString() },
                new SelectListItem { Text = BillStatusEnum.Partial.GetDescription(), Value = BillStatusEnum.Partial.ToInt32ToString() },
                new SelectListItem { Text = BillStatusEnum.Full.GetDescription(), Value = BillStatusEnum.Full.ToInt32ToString() },

            });
        return items;
    }

    #endregion

    #region CustomerListItems
    public IEnumerable<SelectListItem> GetCustomerSelectListItems(bool isDefaultSelectAdd = true)
    {
        var items = GetDefaultSelectListItem(isDefaultSelectAdd);
        var dataList = CacheStore.Get<List<PrCustomer>>(CacheEnum.PrCustomerList.ToString())
            ?? _cacheStoreService.AddOrUpdate<PrCustomer>(CacheEnum.PrCustomerList.ToString());
        items.AddRange(dataList.Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = $"{c.CustomerName}" }));
        return items;
    }
    #endregion

    #region ServiceDynamicData

    public dynamic GetServiceDynamicData(long? categoryId)
    {
        var dataList = CacheStore.Get<List<PrServiceInfo>>(CacheEnum.PrServiceInfoList.ToString()) ?? _cacheStoreService.AddOrUpdate<PrServiceInfo>(CacheEnum.PrServiceInfoList.ToString());
        dataList = categoryId > 0 ? dataList.Where(c => c.CategoryId == categoryId).ToList() : dataList;
        var dynamicData = dataList.Select(c => new { c.Id, Name = c.ServiceName });
        return dynamicData;
    }
    #endregion

    #region BranchListItems
    public IEnumerable<SelectListItem> GetBranchSelectListItems(bool isDefaultSelectAdd = true)
    {
        var items = GetDefaultSelectListItem(isDefaultSelectAdd);
        var dataList = CacheStore.Get<List<Branch>>(CacheEnum.BranchList.ToString())
            ?? _cacheStoreService.AddOrUpdate<Branch>(CacheEnum.BranchList.ToString());

        var filteredList = dataList.Where(c => c.IsDeleted != true);
        items.AddRange(filteredList.Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.BranchName }));
        return items;
    }
    #endregion
}
