using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class RoomProperty
{
    [Key]
    public int Id { get; set; }

    public int RoomId { get; set; }

    public int RoomPropertyTypeId { get; set; }

    [StringLength(50)]
    public string Value { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("RoomId")]
    [InverseProperty("RoomProperties")]
    public virtual Room Room { get; set; } = null!;

    [ForeignKey("RoomPropertyTypeId")]
    [InverseProperty("RoomProperties")]
    public virtual RoomPropertyType RoomPropertyType { get; set; } = null!;
}
