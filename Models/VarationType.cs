using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class VarationType
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    public int ItemId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("ItemId")]
    [InverseProperty("VarationTypes")]
    public virtual Item Item { get; set; } = null!;

    [InverseProperty("VarationType")]
    public virtual ICollection<ItemEntryVaration> ItemEntryVarations { get; set; } = new List<ItemEntryVaration>();

    [InverseProperty("VarationType")]
    public virtual ICollection<Varation> Varations { get; set; } = new List<Varation>();
}
