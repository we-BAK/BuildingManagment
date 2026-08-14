using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class ItemPrice
{
    [Key]
    public int Id { get; set; }

    public int ItemId { get; set; }

    public DateOnly? AppliedDate { get; set; }

    public double MinPrice { get; set; }

    public double MaxPrice { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("ItemId")]
    [InverseProperty("ItemPrices")]
    public virtual Item Item { get; set; } = null!;
}
