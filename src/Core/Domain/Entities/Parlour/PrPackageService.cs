using Domain.Entities.Identity;

namespace Domain.Entities.Parlour;

public class PrPackageService
{
    public long Id { get; set; }
    public DateTime ActionDate { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsEnable { get; set; }


    //--------------FK--------------

    public long ServiceId { get; set; }
    public PrServiceInfo Service { get; set; }
    public long PackageServiceId { get; set; }
    public PrServiceInfo PackageService { get; set; }
    public long ActionById { get; set; }
    public ApplicationUser ActionBy { get; set; }

}