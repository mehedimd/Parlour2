using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Identity;

public class UserLog
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public ApplicationUser User { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime? DateEnd { get; set; }

    [StringLength(100)]
    public string IPAddress { get; set; }

    [StringLength(100)]
    public string PcName { get; set; }
}