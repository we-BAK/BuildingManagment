using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class Employee
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string FullName { get; set; } = null!;

    public int OrganizationId { get; set; }

    public int SexId { get; set; }

    [StringLength(20)]
    public string PhoneNumber { get; set; } = null!;

    [StringLength(100)]
    public string? Email { get; set; }

    public int? UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [InverseProperty("Employee")]
    public virtual ICollection<BuildingEmployee> BuildingEmployees { get; set; } = new List<BuildingEmployee>();

    [ForeignKey("OrganizationId")]
    [InverseProperty("Employees")]
    public virtual Organization Organization { get; set; } = null!;

    [ForeignKey("SexId")]
    [InverseProperty("Employees")]
    public virtual Sex Sex { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Employees")]
    public virtual User? User { get; set; }
}
