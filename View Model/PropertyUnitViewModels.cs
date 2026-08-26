using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BMS.Models;

namespace BMS.Models.ViewModels
{
    // --- Floor View Models ---
    public class FloorFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Building is required.")]
        [Display(Name = "Building")]
        public int BuildingId { get; set; }

        [Required(ErrorMessage = "Floor Name / Number is required.")]
        [Display(Name = "Floor Name / Number")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Floor Number")]
        public int FloorNumber { get; set; }

        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }

    public class FloorPriceViewModel
    {
        public int FloorId { get; set; }
        public string FloorName { get; set; } = string.Empty;
        public string BuildingName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Base Price is required.")]
        [Display(Name = "Base Monthly Price")]
        [Range(0, 1000000, ErrorMessage = "Price must be positive.")]
        public decimal BasePrice { get; set; }

        public List<FloorPrice> ExistingPrices { get; set; } = new List<FloorPrice>();
    }

    // --- Room View Models ---
    public class RoomFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Building is required.")]
        [Display(Name = "Building")]
        public int BuildingId { get; set; }

        [Display(Name = "Floor")]
        public int? FloorId { get; set; }

        [Required(ErrorMessage = "Room Number/Name is required.")]
        [Display(Name = "Room Number / Code")]
        public string RoomNumber { get; set; } = string.Empty;

        [Display(Name = "Area Size (Sq Ft / Sq M)")]
        public decimal Size { get; set; }

        [Display(Name = "Number of Bedrooms")]
        public int Bedrooms { get; set; }

        [Display(Name = "Number of Bathrooms")]
        public int Bathrooms { get; set; }

        [Required(ErrorMessage = "Room Status is required.")]
        [Display(Name = "Status")]
        public int RoomStatueId { get; set; }

        [Display(Name = "Monthly Base Rent")]
        public decimal DefaultRent { get; set; }

        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }

    public class RoomDetailsViewModel
    {
        public Room Room { get; set; } = null!;
        public List<RoomRental> RentalHistory { get; set; } = new List<RoomRental>();
        public List<RoomPrice> PriceHistory { get; set; } = new List<RoomPrice>();
        public List<RoomProperty> Properties { get; set; } = new List<RoomProperty>();
    }

    public class RoomPriceViewModel
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required.")]
        [Display(Name = "Monthly Rent Price")]
        public decimal Price { get; set; }

        [Display(Name = "Security Deposit")]
        public decimal SecurityDeposit { get; set; }

        public List<RoomPrice> PriceHistory { get; set; } = new List<RoomPrice>();
    }

    // --- Shop View Models ---
    public class ShopFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Building is required.")]
        [Display(Name = "Building")]
        public int BuildingId { get; set; }

        [Display(Name = "Floor")]
        public int? FloorId { get; set; }

        [Required(ErrorMessage = "Shop Number/Name is required.")]
        [Display(Name = "Shop Number / Code")]
        public string ShopNumber { get; set; } = string.Empty;

        [Display(Name = "Retail Size (Sq Ft)")]
        public decimal Size { get; set; }

        [Display(Name = "Facade Type / Frontage")]
        public string FacadeType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Shop Status is required.")]
        [Display(Name = "Status")]
        public int ShopStatusId { get; set; }

        [Display(Name = "Monthly Lease Price")]
        public decimal Price { get; set; }

        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }

    public class ShopDetailsViewModel
    {
        public Shop Shop { get; set; } = null!;
        public List<ShopImage> Images { get; set; } = new List<ShopImage>();
        public List<ShopSpecification> Specifications { get; set; } = new List<ShopSpecification>();
        public List<ShopRequest> RentalRequests { get; set; } = new List<ShopRequest>();
    }

    public class ShopImageViewModel
    {
        public int ShopId { get; set; }
        public string ShopNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Image URL or Path is required.")]
        public string ImageUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public List<ShopImage> ExistingImages { get; set; } = new List<ShopImage>();
    }

    public class ShopSpecViewModel
    {
        public int ShopId { get; set; }
        public string ShopNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Requirement/Attribute Title is required.")]
        public string FeatureName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Specification Detail is required.")]
        public string FeatureValue { get; set; } = string.Empty;

        public List<ShopSpecification> ExistingSpecifications { get; set; } = new List<ShopSpecification>();
    }
}
