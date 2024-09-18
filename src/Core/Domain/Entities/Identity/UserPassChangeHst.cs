using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Identity;

public class UserPassChangeHst
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public ApplicationUser User { get; set; }

    [StringLength(40)]
    public string Password { get; set; }
    public DateTime ChangeDate { get; set; }

    [StringLength(100)]
    public string IPAddress { get; set; }

    [StringLength(100)]
    public string PcName { get; set; }
    public long? SessionId { get; set; }
    public UserLog Session { get; set; }
}