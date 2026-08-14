using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class ShopItem
{
    [Key]
    public int Id { get; set; }

    public int ItemId { get; set; }

    public int ShopId { get; set; }

    [StringLength(50)]
    public string Balance { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("ItemId")]
    [InverseProperty("ShopItems")]
    public virtual Item Item { get; set; } = null!;

    [InverseProperty("ShopItem")]
    public virtual ICollection<ItemEntry> ItemEntries { get; set; } = new List<ItemEntry>();

    [InverseProperty("ShopItem")]
    public virtual ICollection<ItemEntryVaration> ItemEntryVarations { get; set; } = new List<ItemEntryVaration>();

    [ForeignKey("ShopId")]
    [InverseProperty("ShopItems")]
    public virtual Shop Shop { get; set; } = null!;
}
