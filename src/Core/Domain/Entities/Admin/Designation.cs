﻿using Domain.Entities.Identity;
using Domain.ModelInterface;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Admin;

public class Designation : IAuditable
{
    public long Id { get; set; }

    [Required]
    [StringLength(80)]
    public string Name { get; set; }

    [Required]
    [StringLength(30)]
    public string Code { get; set; }

    [StringLength(350)]
    public string Remarks { get; set; }
    public DateTime ActionDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool IsDeleted { get; set; }

    //------------------FK--------------------

    public long ActionById { get; set; }
    public ApplicationUser ActionBy { get; set; }
    public long? UpdatedById { get; set; }
    public ApplicationUser UpdatedBy { get; set; }
}
