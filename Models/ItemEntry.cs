using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class ItemEntry
{
    [Key]
    public int Id { get; set; }

    public int ShopItemId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime EntryDate { get; set; }

    public double Quantity { get; set; }

    public double? WithdrawQuantity { get; set; }

    public double? Price { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [InverseProperty("ItemEntry")]
    public virtual ICollection<ItemEntryVaration> ItemEntryVarations { get; set; } = new List<ItemEntryVaration>();

    [ForeignKey("ShopItemId")]
    [InverseProperty("ItemEntries")]
    public virtual ShopItem ShopItem { get; set; } = null!;

    [InverseProperty("Entry")]
    public virtual ICollection<Withdrawl> Withdrawls { get; set; } = new List<Withdrawl>();
}
