using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class ShopLocation
{
    [Key]
    public int Id { get; set; }

    public int ShopId { get; set; }

    public int RoomId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CeratedDate { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("RoomId")]
    [InverseProperty("ShopLocations")]
    public virtual Room Room { get; set; } = null!;

    [ForeignKey("ShopId")]
    [InverseProperty("ShopLocations")]
    public virtual Shop Shop { get; set; } = null!;
}
