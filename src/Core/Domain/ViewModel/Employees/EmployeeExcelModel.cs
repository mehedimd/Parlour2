namespace Domain.ViewModel.Employees;

public class EmployeeExcelModel
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string Designation { get; set; }
    public string Department { get; set; }
    public string AcademicDepartment { get; set; }
    public string Gender { get; set; }
}

public class EmpMachineIdExcelModel
{
    public string EmployeeCode { get; set; }
    public string Name { get; set; }
    public string MachineId { get; set; }
}
