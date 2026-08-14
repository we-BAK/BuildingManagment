using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class UserLogon
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    [StringLength(50)]
    public string FingerPrint { get; set; } = null!;

    [StringLength(250)]
    public string UserAgent { get; set; } = null!;

    [StringLength(50)]
    public string Platform { get; set; } = null!;

    [StringLength(50)]
    public string Browser { get; set; } = null!;

    [StringLength(50)]
    public string TimeZone { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime LogDate { get; set; }

    [StringLength(8)]
    public string VerificationCode { get; set; } = null!;

    public bool IsVerified { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("UserLogons")]
    public virtual User User { get; set; } = null!;
}
