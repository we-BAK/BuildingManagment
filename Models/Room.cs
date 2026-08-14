using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class Room
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    public int FloorId { get; set; }

    public int UserId { get; set; }

    public int RoomStatusId { get; set; }

    public int SizeInm2 { get; set; }

    public double Width { get; set; }

    public double Length { get; set; }

    [StringLength(100)]
    public string Description { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("FloorId")]
    [InverseProperty("Rooms")]
    public virtual Floor Floor { get; set; } = null!;

    [InverseProperty("Room")]
    public virtual ICollection<MaintenanceRequest> MaintenanceRequests { get; set; } = new List<MaintenanceRequest>();

    [InverseProperty("Room")]
    public virtual ICollection<RoomPrice> RoomPrices { get; set; } = new List<RoomPrice>();

    [InverseProperty("Room")]
    public virtual ICollection<RoomProperty> RoomProperties { get; set; } = new List<RoomProperty>();

    [InverseProperty("Room")]
    public virtual ICollection<RoomRentalRequest> RoomRentalRequests { get; set; } = new List<RoomRentalRequest>();

    [InverseProperty("Room")]
    public virtual ICollection<RoomRental> RoomRentals { get; set; } = new List<RoomRental>();

    [ForeignKey("RoomStatusId")]
    [InverseProperty("Rooms")]
    public virtual RoomStatue RoomStatus { get; set; } = null!;

    [InverseProperty("Room")]
    public virtual ICollection<ShopLocation> ShopLocations { get; set; } = new List<ShopLocation>();

    [ForeignKey("UserId")]
    [InverseProperty("Rooms")]
    public virtual User User { get; set; } = null!;
}
