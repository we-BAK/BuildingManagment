using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class BuildingImage
{
    [Key]
    public int Id { get; set; }

    public int BuildingId { get; set; }

    [StringLength(250)]
    public string? Url { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("BuildingId")]
    [InverseProperty("BuildingImages")]
    public virtual Building Building { get; set; } = null!;
}
