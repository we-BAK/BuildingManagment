using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class ShortcutMenu
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string Title { get; set; } = null!;

    public int? ApplicationId { get; set; }

    public int? MenuId { get; set; }

    [Column("URL")]
    [StringLength(250)]
    public string? Url { get; set; }

    [StringLength(50)]
    public string Icon { get; set; } = null!;

    public string Discerption { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("ApplicationId")]
    [InverseProperty("ShortcutMenus")]
    public virtual Application? Application { get; set; }

    [ForeignKey("MenuId")]
    [InverseProperty("ShortcutMenus")]
    public virtual Menu? Menu { get; set; }
}
