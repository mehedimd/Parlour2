using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Identity;

public class UserLogAttempt
{
    public long Id { get; set; }

    [StringLength(100)]
    public string UserName { get; set; }

    [StringLength(200)]
    public string UserFullName { get; set; }

    [StringLength(40)]
    public string UserPassword { get; set; }
    public DateTime DateAttempt { get; set; }

    [StringLength(100)]
    public string IPAddress { get; set; }

    [StringLength(100)]
    public string PcName { get; set; }
    public long? SessionId { get; set; }
    public UserLog Session { get; set; }
}
