using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class UserPassword
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    [StringLength(150)]
    public string Password { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime ChangedDate { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("UserPasswords")]
    public virtual User User { get; set; } = null!;
}
