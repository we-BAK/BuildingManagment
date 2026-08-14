using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class Language
{
    [Key]
    public int Id { get; set; }

    [StringLength(25)]
    public string Name { get; set; } = null!;

    [StringLength(10)]
    public string? LangudgeCode { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [InverseProperty("DefaultLanguage")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
