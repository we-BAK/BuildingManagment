using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class BuildingRequest
{
    [Key]
    public int Id { get; set; }

    public int? OrganizationId { get; set; }

    public int BuildingTypeId { get; set; }

    public string? Description { get; set; }

    [StringLength(20)]
    public string NumberOfBuildings { get; set; } = null!;

    public int? CityId { get; set; }

    public int? LocationId { get; set; }

    public int RequestStatusId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime RequestedDate { get; set; }

    public int UserId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("BuildingTypeId")]
    [InverseProperty("BuildingRequests")]
    public virtual BuildingType BuildingType { get; set; } = null!;

    [ForeignKey("CityId")]
    [InverseProperty("BuildingRequests")]
    public virtual City? City { get; set; }

    [ForeignKey("LocationId")]
    [InverseProperty("BuildingRequests")]
    public virtual Location? Location { get; set; }

    [ForeignKey("OrganizationId")]
    [InverseProperty("BuildingRequests")]
    public virtual Organization? Organization { get; set; }

    [ForeignKey("RequestStatusId")]
    [InverseProperty("BuildingRequests")]
    public virtual BuildingRequestStatus RequestStatus { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("BuildingRequests")]
    public virtual User User { get; set; } = null!;
}
