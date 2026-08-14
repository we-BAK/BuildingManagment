using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class RoomPropertyTypeOption
{
    [Key]
    public int Id { get; set; }

    public int RoomPropertyTypeId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("RoomPropertyTypeId")]
    [InverseProperty("RoomPropertyTypeOptions")]
    public virtual RoomPropertyType RoomPropertyType { get; set; } = null!;
}
