using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class UserEmail
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    [StringLength(150)]
    public string Email { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("UserEmails")]
    public virtual User User { get; set; } = null!;
}
