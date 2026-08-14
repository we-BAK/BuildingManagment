using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class OrganizationUser
{
    [Key]
    public int Id { get; set; }

    public int OrganizationId { get; set; }

    public int UserId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("OrganizationId")]
    [InverseProperty("OrganizationUsers")]
    public virtual Organization Organization { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("OrganizationUsers")]
    public virtual User User { get; set; } = null!;
}
