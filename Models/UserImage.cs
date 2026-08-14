using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class UserImage
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    [StringLength(150)]
    public string Url { get; set; } = null!;

    [StringLength(150)]
    public string? ImageDescription { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("UserImages")]
    public virtual User User { get; set; } = null!;
}
