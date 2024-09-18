namespace Domain.Utility.Common;

public static class ConstantsValue
{
    public static UserRoleName UserRoleName => new UserRoleName();
    public static RolePermission RolePermission => new RolePermission();
}

public static class ExtraBedAmount
{
    public static short ExtraBedSingleCharge => 500;
}

public class UserRoleName
{
    public string SuperAdmin => "SuperAdmin";
    public string Admin => "Admin";
    public string NormalUser => "Employee";
    public string DistrictTrainer => "Teacher";
    public string Entrepreneur => "Student";

}

public class RolePermission
{
    public string Type => "Permission";
    public string Value => "Permissions.SuperAdmin";
}

public static class PrintInfo
{
    public static string CompanyName => "Lia's Beauty Box";
    public static string CompanyAddress => "Plot-54, Road-11, Block-C (2nd Floor), Banani, Dhaka-1213, Dhaka Division, Bangladesh";
    public static string PrincipalName => "";
    public static string PrincipalQualification => "";

}

public static class PrSalaryPartValueType
{
    public static string Amount => "A";
    public static string Percent => "P";
}

public static class PrSalaryPartType
{
    public static string Addition => "A";
    public static string Deduction => "D";
}

public static class PrSalaryPartCol
{
    public static string ColA => "ColA";
    public static string ColB => "ColB";
    public static string ColC => "ColC";
    public static string ColD => "ColD";
    public static string ColE => "ColE";
    public static string ColF => "ColF";
    public static string ColG => "ColG";
    public static string ColH => "ColH";
    public static string ColI => "ColI";
    public static string ColJ => "ColJ";
    public static string ColK => "ColK";
    public static string ColL => "ColL";
    public static string ColM => "ColM";
    public static string ColN => "ColN";
    public static string ColO => "ColO";
    public static string ColP => "ColP";
    public static string ColQ => "ColQ";
    public static string ColR => "ColR";
    public static string ColS => "ColS";
    public static string ColT => "ColT";
    public static string ColU => "ColU";
}

public static class PrSalaryPartLink
{
    public static string Basic => "B";
    public static string Arrear => "A";
    public static string Loan => "L";
    public static string Pf => "P";
    public static string SalaryDeduct => "D";
    public static string GratuityA => "G";
    public static string GratuityD => "R";
    public static string LoanInterest => "I";
}

public static class EmpAttendanceStatus
{
    public static string Present => "P";
    public static string Absent => "A";
    public static string Leave => "L";
    public static string Holiday => "H";
    public static string Offday => "O";

}

public static class VoucherType
{
    public static string OpeningVoucher => "O";
    public static string JournalVoucher => "J";
    public static string BankDebitVoucher => "D";
    public static string CashDebitVoucher => "S";
    public static string CashCreditVoucher => "H";
    public static string BankCreditVoucher => "C";
    public static string PettyCash => "P";
}

public static class VoucherTypeCode
{
    public static string OpeningVoucher => "OP";
    public static string JournalVoucher => "JV";
    public static string BankDebitVoucher => "BD";
    public static string CashDebitVoucher => "CD";
    public static string CashCreditVoucher => "CC";
    public static string BankCreditVoucher => "BC";
}

public static class PfReportType
{
    public static string Monthly => "M";
    public static string Interest => "I";
    public static string Profit => "P";
    public static string Others => "O";
}

public static class ExamResultStatus
{
    public static string Pass => "PASS";
    public static string Fail => "FAIL";
}

public static class VcNoteType
{
    public static string Create => "Create";
    public static string Approve => "Approve";
    public static string Audit => "Audit";
    public static string BankClear => "BankClear";
    public static string FinalAttach => "FinalAttach";
    public static string Note => "Note";
    public static string AutoCreate => "AutoCreate";

}

public static class AdvanceFeeType
{
    public static string Code => "EXTRA";

}

public static class FeeTypeCode
{
    public static string TuitionFee => "TUITION";
    public static string FineTuitionFee => "FEE-06";
    public static string HostelFee => "HOSTEL";
    public static string AdvanceFeeType => "EXTRA";
    public static string FieldVisit => "FIELD";
    public static string ClinicalFeeOne => "CLINICAL01";
    public static string ClinicalFeeTwo => "CLINICAL02";
    public static string ClinicalFeeThree => "CLINICAL03";
    public static string MiscFee => "FEE-11";
    public static string BmdcRegFee => "FEE-14";

}

public static class ClassTypeMBBS
{
    public static short LectureClasses => 1;
    public static short TutorialOrPracticalOrDemonstrationOrSeminarAndClinicalClasses => 2;
    public static short CommunityBasedMedicalEducation => 4;
    public static short QualifyingMarksInEachTheoreticalSubject => 60;
    public static short QualifyingMarksInEachOralPracticalClinicalSubject => 60;

}
public static class ClassTypeBDS
{
    public static short LectureClasses => 1;
    public static short TutorialOrPracticalOrDemonstrationOrSeminarAndClinicalClasses => 2;
    public static short CommunityBasedMedicalEducation => 4;
    public static short QualifyingMarksInEachTheoreticalSubject => 60;
    public static short QualifyingMarksInEachOralPracticalClinicalSubject => 60;

}

public static class AccHeadCode
{
    public static string StudentCollection => "2.1.1";
}

public static class AccLadgerCode
{
    public static string RoomRentReceive => "L2.1.1.1";
    public static string RoomAdvanceRentReceive => "L3.2.1.1.1";//"L2.1.1.2";
    public static string CityBankLtd => "L1.2.5.2.1.1";
    public static string CashInHand => "L1.2.5.1.1";
}

public static class CountryCode
{
    public static string LocalCountry => "BD";
}

public static class CourseCode
{
    public static string MBBS => "MBBS";
    public static string BDS => "BDS";
}

public static class TransType
{
    public const string Receive = "R";
    public const string Issue = "I";
    public const string Opening = "O";
    public const string Scrap = "S";
    public const string Return = "U";
    public const string IssueReturn = "E";
    public const string Consumption = "C";
    public const string LeftOver = "L";
}

public static class HtServiceCode
{
    public static string RoomRent => "SR000001";
    public static string FoodService => "SR000002";
    public static string HallRent => "SR000004";
}

public static class RsCustomerTypeCode
{
    public static string Hotel => "Hotel";
    public static string WalkIn => "Walk-In";
    public static string Employee => "Employee";
    public static string Online => "Online";
    public static string Complementary => "Complementary";
    public static string MD_Sir => "MD";
    public static string GM_Sir => "GM";
}

public static class DepartmentCode
{
    public static string FrontDesk => "DPT-01";
    public static string HouseKeeper => "DPT-02";
    public static string Resturant => "DPT-03";
    public static string Accounting => "DPT-04";
    public static string HR => "DPT-05";
}

public static class DesignationCode
{
    public static string HouseKeeper => "DES-01";
    public static string Waiter => "DES-02";
}

public static class BookingType
{
    public static string Room => "R";
    public static string Hall => "H";
}

public static class TranType
{
    public const string Receive = "R";
    public const string Payemnt = "P";
}