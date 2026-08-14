using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class UserPasswordReset
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    [StringLength(100)]
    public string Token { get; set; } = null!;

    [StringLength(12)]
    public string VerificationCode { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime ExpiryDate { get; set; }

    public bool Validated { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("UserPasswordResets")]
    public virtual User User { get; set; } = null!;
}
