using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class Application
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(20)]
    public string? Acronym { get; set; }

    [StringLength(15)]
    public string? Icon { get; set; }

    [StringLength(50)]
    public string? Code { get; set; }

    [StringLength(20)]
    public string? Color { get; set; }

    [StringLength(10)]
    public string? Version { get; set; }

    public string? Description { get; set; }

    [Column("URL")]
    [StringLength(100)]
    public string? Url { get; set; }

    public int? SortOrder { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [InverseProperty("Application")]
    public virtual ICollection<MenuCategory> MenuCategories { get; set; } = new List<MenuCategory>();

    [InverseProperty("Application")]
    public virtual ICollection<ShortcutMenu> ShortcutMenus { get; set; } = new List<ShortcutMenu>();
}
