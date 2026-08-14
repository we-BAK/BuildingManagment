using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class ShopSpecification
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    public int ShopRequestId { get; set; }

    public int UseTypeId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("ShopRequestId")]
    [InverseProperty("ShopSpecifications")]
    public virtual ShopRequest ShopRequest { get; set; } = null!;

    [ForeignKey("UseTypeId")]
    [InverseProperty("ShopSpecifications")]
    public virtual UseType UseType { get; set; } = null!;
}
