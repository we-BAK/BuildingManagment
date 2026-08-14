using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

[Keyless]
public partial class RoleApplication
{
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(15)]
    public string? Icon { get; set; }

    [StringLength(50)]
    public string? Code { get; set; }

    [StringLength(10)]
    public string? Version { get; set; }

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public int RoleId { get; set; }

    [StringLength(50)]
    public string RoleName { get; set; } = null!;
}
