using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class BuildingEmployee
{
    [Key]
    public int Id { get; set; }

    public int BuildingId { get; set; }

    public int EmployeeId { get; set; }

    public int EmployeeTypeId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("BuildingId")]
    [InverseProperty("BuildingEmployees")]
    public virtual Building Building { get; set; } = null!;

    [ForeignKey("EmployeeId")]
    [InverseProperty("BuildingEmployees")]
    public virtual Employee Employee { get; set; } = null!;

    [ForeignKey("EmployeeTypeId")]
    [InverseProperty("BuildingEmployees")]
    public virtual EmployeeType EmployeeType { get; set; } = null!;

    [InverseProperty("BuildingEmployee")]
    public virtual ICollection<MaintenanceRequestAllocation> MaintenanceRequestAllocations { get; set; } = new List<MaintenanceRequestAllocation>();
}
