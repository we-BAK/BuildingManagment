using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMS.Models;

public partial class Building
{
    [Key]
    public int Id { get; set; }

    public int OrganizationId { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int BuildingTypeId { get; set; }

    public int? UseTypeId { get; set; }

    public int? CityId { get; set; }

    public int? LocationId { get; set; }

    [StringLength(20)]
    public string? ConstractionYear { get; set; }

    public int NumberOfFloors { get; set; }

    public int UserId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [InverseProperty("Building")]
    public virtual ICollection<BuildingEmployee> BuildingEmployees { get; set; } = new List<BuildingEmployee>();

    [InverseProperty("Building")]
    public virtual ICollection<BuildingImage> BuildingImages { get; set; } = new List<BuildingImage>();

    [ForeignKey("BuildingTypeId")]
    [InverseProperty("Buildings")]
    public virtual BuildingType BuildingType { get; set; } = null!;

    [ForeignKey("CityId")]
    [InverseProperty("Buildings")]
    public virtual City? City { get; set; }

    [InverseProperty("Building")]
    public virtual ICollection<Floor> Floors { get; set; } = new List<Floor>();

    [ForeignKey("LocationId")]
    [InverseProperty("Buildings")]
    public virtual Location? Location { get; set; }

    [ForeignKey("OrganizationId")]
    [InverseProperty("Buildings")]
    public virtual Organization Organization { get; set; } = null!;

    [InverseProperty("Building")]
    public virtual ICollection<Tenant> Tenants { get; set; } = new List<Tenant>();

    [ForeignKey("UseTypeId")]
    [InverseProperty("Buildings")]
    public virtual UseType? UseType { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Buildings")]
    public virtual User User { get; set; } = null!;
}
