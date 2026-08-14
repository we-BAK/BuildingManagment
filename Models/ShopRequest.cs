using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class ShopRequest
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    [StringLength(200)]
    public string Description { get; set; } = null!;

    public int NumberOfShops { get; set; }

    public int RequestStatusId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime RequestDate { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("RequestStatusId")]
    [InverseProperty("ShopRequests")]
    public virtual ShopRequestStatus RequestStatus { get; set; } = null!;

    [InverseProperty("ShopRequest")]
    public virtual ICollection<ShopSpecification> ShopSpecifications { get; set; } = new List<ShopSpecification>();

    [ForeignKey("UserId")]
    [InverseProperty("ShopRequests")]
    public virtual User User { get; set; } = null!;
}
