using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class Menu
{
    [Key]
    public int Id { get; set; }

    public int MenuCategoryId { get; set; }

    [StringLength(50)]
    public string Title { get; set; } = null!;

    [StringLength(50)]
    public string Controller { get; set; } = null!;

    [StringLength(50)]
    public string Action { get; set; } = null!;

    public int? OrderNumber { get; set; }

    public bool IsMenu { get; set; }

    public bool IsTraceable { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("MenuCategoryId")]
    [InverseProperty("Menus")]
    public virtual MenuCategory MenuCategory { get; set; } = null!;

    [InverseProperty("Menu")]
    public virtual ICollection<RolesMenu> RolesMenus { get; set; } = new List<RolesMenu>();

    [InverseProperty("Menu")]
    public virtual ICollection<ShortcutMenu> ShortcutMenus { get; set; } = new List<ShortcutMenu>();
}
