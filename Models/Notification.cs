using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class Notification
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    [StringLength(500)]
    public string Message { get; set; } = null!;

    public int NotificationTypeId { get; set; }

    public int NotificationStatusId { get; set; }

    [StringLength(200)]
    public string? Url { get; set; }

    public DateOnly NotificationDate { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("NotificationStatusId")]
    [InverseProperty("Notifications")]
    public virtual NotificationStatus NotificationStatus { get; set; } = null!;

    [ForeignKey("NotificationTypeId")]
    [InverseProperty("Notifications")]
    public virtual NotificationType NotificationType { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Notifications")]
    public virtual User User { get; set; } = null!;
}
