using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class UseType
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(200)]
    public string Description { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [InverseProperty("UseType")]
    public virtual ICollection<Building> Buildings { get; set; } = new List<Building>();

    [InverseProperty("UseType")]
    public virtual ICollection<ShopSpecification> ShopSpecifications { get; set; } = new List<ShopSpecification>();
}
