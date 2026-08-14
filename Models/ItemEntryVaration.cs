using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class ItemEntryVaration
{
    [Key]
    public int Id { get; set; }

    public int VarationTypeId { get; set; }

    public int VarationId { get; set; }

    public int ShopItemId { get; set; }

    public int ItemEntryId { get; set; }

    public double Quantity { get; set; }

    public double Price { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("ItemEntryId")]
    [InverseProperty("ItemEntryVarations")]
    public virtual ItemEntry ItemEntry { get; set; } = null!;

    [ForeignKey("ShopItemId")]
    [InverseProperty("ItemEntryVarations")]
    public virtual ShopItem ShopItem { get; set; } = null!;

    [ForeignKey("VarationId")]
    [InverseProperty("ItemEntryVarations")]
    public virtual Varation Varation { get; set; } = null!;

    [ForeignKey("VarationTypeId")]
    [InverseProperty("ItemEntryVarations")]
    public virtual VarationType VarationType { get; set; } = null!;
}
