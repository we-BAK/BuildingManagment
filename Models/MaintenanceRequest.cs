using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class MaintenanceRequest
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    public int RoomId { get; set; }

    public int MaintenanceTypeId { get; set; }

    public int MaintenanceStatusId { get; set; }

    [StringLength(150)]
    public string Description { get; set; } = null!;

    public DateOnly DateSubmitted { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [InverseProperty("MaintenanceRequest")]
    public virtual ICollection<MaintenanceRequestAllocation> MaintenanceRequestAllocations { get; set; } = new List<MaintenanceRequestAllocation>();

    [ForeignKey("MaintenanceStatusId")]
    [InverseProperty("MaintenanceRequests")]
    public virtual MaintenanceStatus MaintenanceStatus { get; set; } = null!;

    [ForeignKey("MaintenanceTypeId")]
    [InverseProperty("MaintenanceRequests")]
    public virtual MaintenanceType MaintenanceType { get; set; } = null!;

    [ForeignKey("RoomId")]
    [InverseProperty("MaintenanceRequests")]
    public virtual Room Room { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("MaintenanceRequests")]
    public virtual User User { get; set; } = null!;
}
