using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class Item
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    public int UserId { get; set; }

    [StringLength(250)]
    public string? Description { get; set; }

    public int ItemCategoryId { get; set; }

    public bool IsAvailable { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("ItemCategoryId")]
    [InverseProperty("Items")]
    public virtual ItemCategory ItemCategory { get; set; } = null!;

    [InverseProperty("Item")]
    public virtual ICollection<ItemImage> ItemImages { get; set; } = new List<ItemImage>();

    [InverseProperty("Item")]
    public virtual ICollection<ItemPrice> ItemPrices { get; set; } = new List<ItemPrice>();

    [InverseProperty("Item")]
    public virtual ICollection<ShopItem> ShopItems { get; set; } = new List<ShopItem>();

    [ForeignKey("UserId")]
    [InverseProperty("Items")]
    public virtual User User { get; set; } = null!;

    [InverseProperty("Item")]
    public virtual ICollection<VarationType> VarationTypes { get; set; } = new List<VarationType>();
}
