using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class MaintenanceStatusReport
{
    [Key]
    public int Id { get; set; }

    public int MaintenanceRequestAllocationId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ReportDate { get; set; }

    [StringLength(150)]
    public string Remark { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("MaintenanceRequestAllocationId")]
    [InverseProperty("MaintenanceStatusReports")]
    public virtual MaintenanceRequestAllocation MaintenanceRequestAllocation { get; set; } = null!;
}
