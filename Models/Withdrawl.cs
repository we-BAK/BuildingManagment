using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class Withdrawl
{
    [Key]
    public int Id { get; set; }

    public int EntryId { get; set; }

    public double Quantity { get; set; }

    [StringLength(250)]
    public string Remark { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime Date { get; set; }

    public int ActionBy { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("ActionBy")]
    [InverseProperty("Withdrawls")]
    public virtual User ActionByNavigation { get; set; } = null!;

    [ForeignKey("EntryId")]
    [InverseProperty("Withdrawls")]
    public virtual ItemEntry Entry { get; set; } = null!;
}
