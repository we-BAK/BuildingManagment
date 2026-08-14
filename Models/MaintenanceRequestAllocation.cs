using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class MaintenanceRequestAllocation
{
    [Key]
    public int Id { get; set; }

    public int MaintenanceRequestId { get; set; }

    public int BuildingEmployeeId { get; set; }

    public int? AllocationStatusId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime AllocatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CompletedDate { get; set; }

    public int? AprovedBy { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("AllocationStatusId")]
    [InverseProperty("MaintenanceRequestAllocations")]
    public virtual MaintenanceAllocationStatus? AllocationStatus { get; set; }

    [ForeignKey("AprovedBy")]
    [InverseProperty("MaintenanceRequestAllocations")]
    public virtual User? AprovedByNavigation { get; set; }

    [ForeignKey("BuildingEmployeeId")]
    [InverseProperty("MaintenanceRequestAllocations")]
    public virtual BuildingEmployee BuildingEmployee { get; set; } = null!;

    [ForeignKey("MaintenanceRequestId")]
    [InverseProperty("MaintenanceRequestAllocations")]
    public virtual MaintenanceRequest MaintenanceRequest { get; set; } = null!;

    [InverseProperty("MaintenanceRequestAllocation")]
    public virtual ICollection<MaintenanceStatusReport> MaintenanceStatusReports { get; set; } = new List<MaintenanceStatusReport>();
}
