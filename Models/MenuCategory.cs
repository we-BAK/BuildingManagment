using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

[Table("MenuCategory")]
public partial class MenuCategory
{
    [Key]
    public int Id { get; set; }

    [StringLength(150)]
    public string Name { get; set; } = null!;

    public int? ApplicationId { get; set; }

    public int MenuTypeId { get; set; }

    [StringLength(50)]
    public string? Icon { get; set; }

    public int? ParentId { get; set; }

    public int? OrderNumber { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("ApplicationId")]
    [InverseProperty("MenuCategories")]
    public virtual Application? Application { get; set; }

    [InverseProperty("Parent")]
    public virtual ICollection<MenuCategory> InverseParent { get; set; } = new List<MenuCategory>();

    [ForeignKey("MenuTypeId")]
    [InverseProperty("MenuCategories")]
    public virtual MenuType MenuType { get; set; } = null!;

    [InverseProperty("MenuCategory")]
    public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();

    [ForeignKey("ParentId")]
    [InverseProperty("InverseParent")]
    public virtual MenuCategory? Parent { get; set; }
}
