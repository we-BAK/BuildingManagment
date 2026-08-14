using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class TenantUser
{
    [Key]
    public int Id { get; set; }

    public int TenantId { get; set; }

    public int UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("TenantUserCreatedByNavigations")]
    public virtual User? CreatedByNavigation { get; set; }

    [ForeignKey("TenantId")]
    [InverseProperty("TenantUsers")]
    public virtual Tenant Tenant { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("TenantUserUsers")]
    public virtual User User { get; set; } = null!;
}
