namespace Domain.ViewModel.EmpAttendence;

public class EmpAttendanceExcelModel
{
    public string MachineId { get; set; }
    public string Name { get; set; }
    public string Date { get; set; }
    public string OnDuty { get; set; }
    public string OffDuty { get; set; }
    public string EntryTime { get; set; }
    public string ExitTime { get; set; }
    public string Absent { get; set; }
}
