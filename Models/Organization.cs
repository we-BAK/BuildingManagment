using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class Organization
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string Name { get; set; } = null!;

    public int OrganizationTypeId { get; set; }

    [Column("TIN")]
    public int Tin { get; set; }

    public int DocumentId { get; set; }

    public bool Verified { get; set; }

    public DateOnly RegisteredDate { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [InverseProperty("Organization")]
    public virtual ICollection<BuildingRequest> BuildingRequests { get; set; } = new List<BuildingRequest>();

    [InverseProperty("Organization")]
    public virtual ICollection<Building> Buildings { get; set; } = new List<Building>();

    [ForeignKey("DocumentId")]
    [InverseProperty("Organizations")]
    public virtual Documente Document { get; set; } = null!;

    [InverseProperty("Organization")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    [ForeignKey("OrganizationTypeId")]
    [InverseProperty("Organizations")]
    public virtual OrganizationType OrganizationType { get; set; } = null!;

    [InverseProperty("Organization")]
    public virtual ICollection<OrganizationUser> OrganizationUsers { get; set; } = new List<OrganizationUser>();
}
