using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BMS.Models;

namespace BMS.Models.ViewModels
{
    public class BuildingFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Building Name is required.")]
        [Display(Name = "Building Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Building Type is required.")]
        [Display(Name = "Building Type")]
        public int BuildingTypeId { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        public string Location { get; set; } = string.Empty;

        [Display(Name = "Business Area")]
        public int? BusinessAreaId { get; set; }

        [Display(Name = "City")]
        public int? CityId { get; set; }

        [Display(Name = "Use Type")]
        public int? UseTypeId { get; set; }

        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }

    public class BuildingDetailsViewModel
    {
        public Building Building { get; set; } = null!;
        public List<Floor> Floors { get; set; } = new List<Floor>();
        public List<Room> Rooms { get; set; } = new List<Room>();
        public List<Shop> Shops { get; set; } = new List<Shop>();
        public List<BuildingImage> Images { get; set; } = new List<BuildingImage>();
        public List<BuildingSpecification> Specifications { get; set; } = new List<BuildingSpecification>();
    }

    public class BuildingImageUploadViewModel
    {
        public int BuildingId { get; set; }
        public string BuildingName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter an Image URL or path.")]
        [Display(Name = "Image URL or File Path")]
        public string ImageUrl { get; set; } = string.Empty;

        [Display(Name = "Caption / Description")]
        public string Description { get; set; } = string.Empty;

        public List<BuildingImage> ExistingImages { get; set; } = new List<BuildingImage>();
    }

    public class BuildingSpecViewModel
    {
        public int BuildingId { get; set; }
        public string BuildingName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Specification Title is required.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Specification Details are required.")]
        public string Detail { get; set; } = string.Empty;

        public List<BuildingSpecification> Specifications { get; set; } = new List<BuildingSpecification>();
    }

    public class BuildingTypeFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Type Name is required.")]
        [Display(Name = "Type Name")]
        public string Name { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
        public List<BuildingType> ExistingTypes { get; set; } = new List<BuildingType>();
    }
}
