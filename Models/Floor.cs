using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class Floor
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    public int BuildingId { get; set; }

    [StringLength(50)]
    public string NumberOfRoom { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("BuildingId")]
    [InverseProperty("Floors")]
    public virtual Building Building { get; set; } = null!;

    [InverseProperty("Floor")]
    public virtual ICollection<FloorPrice> FloorPrices { get; set; } = new List<FloorPrice>();

    [InverseProperty("Floor")]
    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
