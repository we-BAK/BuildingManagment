using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class RoomRentalRequest
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    [StringLength(200)]
    public string? Description { get; set; }

    public int RoomId { get; set; }

    public int RequestStatusId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime RequestedDate { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("RequestStatusId")]
    [InverseProperty("RoomRentalRequests")]
    public virtual RoomRequestStatus RequestStatus { get; set; } = null!;

    [ForeignKey("RoomId")]
    [InverseProperty("RoomRentalRequests")]
    public virtual Room Room { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("RoomRentalRequests")]
    public virtual User User { get; set; } = null!;
}
