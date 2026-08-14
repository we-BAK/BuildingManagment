using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class BuildingType
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [InverseProperty("BuildingType")]
    public virtual ICollection<BuildingRequest> BuildingRequests { get; set; } = new List<BuildingRequest>();

    [InverseProperty("BuildingType")]
    public virtual ICollection<Building> Buildings { get; set; } = new List<Building>();
}
