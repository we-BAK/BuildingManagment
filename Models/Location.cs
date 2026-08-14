using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class Location
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
    public string? Coordinates { get; set; }

    public int CityId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [InverseProperty("Location")]
    public virtual ICollection<BuildingRequest> BuildingRequests { get; set; } = new List<BuildingRequest>();

    [InverseProperty("Location")]
    public virtual ICollection<Building> Buildings { get; set; } = new List<Building>();

    [ForeignKey("CityId")]
    [InverseProperty("Locations")]
    public virtual City City { get; set; } = null!;
}
