using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class RolesMenu
{
    [Key]
    public int Id { get; set; }

    public int RoleId { get; set; }

    public int MenuId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("MenuId")]
    [InverseProperty("RolesMenus")]
    public virtual Menu Menu { get; set; } = null!;

    [ForeignKey("RoleId")]
    [InverseProperty("RolesMenus")]
    public virtual Role Role { get; set; } = null!;
}
