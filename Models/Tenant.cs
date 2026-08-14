using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class Tenant
{
    [Key]
    public int Id { get; set; }

    public int BuildingId { get; set; }

    public int? Tin { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
    public string Description { get; set; } = null!;

    [StringLength(50)]
    public string Contact { get; set; } = null!;

    public int TenantTypeId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("BuildingId")]
    [InverseProperty("Tenants")]
    public virtual Building Building { get; set; } = null!;

    [InverseProperty("Tenant")]
    public virtual ICollection<RoomRental> RoomRentals { get; set; } = new List<RoomRental>();

    [ForeignKey("TenantTypeId")]
    [InverseProperty("Tenants")]
    public virtual TenantType TenantType { get; set; } = null!;

    [InverseProperty("Tenant")]
    public virtual ICollection<TenantUser> TenantUsers { get; set; } = new List<TenantUser>();
}
