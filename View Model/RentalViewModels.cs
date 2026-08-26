using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BMS.Models;

namespace BMS.Models.ViewModels
{
    public class RentalRequestReviewViewModel
    {
        public int RequestId { get; set; }
        public string RequestType { get; set; } = "Room"; // "Room" or "Shop"
        public string ApplicantName { get; set; } = string.Empty;
        public string ApplicantEmail { get; set; } = string.Empty;
        public string ApplicantPhone { get; set; } = string.Empty;
        public string PropertyName { get; set; } = string.Empty;
        public DateTime RequestDate { get; set; }
        public string CurrentStatus { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;

        [Display(Name = "Staff Decision Notes / Remarks")]
        public string ReviewNotes { get; set; } = string.Empty;
    }

    public class RoomRentalFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Room selection is required.")]
        [Display(Name = "Select Room")]
        public int RoomId { get; set; }

        [Required(ErrorMessage = "Tenant selection is required.")]
        [Display(Name = "Select Tenant")]
        public int TenantId { get; set; }

        [Required(ErrorMessage = "Start Date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Lease Start Date")]
        public DateTime StartDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "End Date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Lease End Date")]
        public DateTime EndDate { get; set; } = DateTime.Today.AddYears(1);

        [Required(ErrorMessage = "Monthly Rent Amount is required.")]
        [Display(Name = "Monthly Rent ($)")]
        public decimal MonthlyRent { get; set; }

        [Display(Name = "Security Deposit ($)")]
        public decimal SecurityDeposit { get; set; }

        public string Remarks { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }

    public class TenantFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name is required.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Identity/National ID is required.")]
        [Display(Name = "National ID / Passport Number")]
        public string IdentityCardNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email Address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone Number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string Phone { get; set; } = string.Empty;

        [Display(Name = "Company / Organization Name")]
        public string CompanyName { get; set; } = string.Empty;

        [Display(Name = "Tenant Type")]
        public int? TenantTypeId { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class TerminationRequestViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Rental Agreement selection is required.")]
        [Display(Name = "Rental Lease Agreement")]
        public int RoomRentalId { get; set; }

        [Required(ErrorMessage = "Termination Date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Requested Termination Date")]
        public DateTime RequestedTerminationDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Reason for termination is required.")]
        [Display(Name = "Reason for Termination")]
        public string Reason { get; set; } = string.Empty;
    }

    public class TerminationApprovalViewModel
    {
        public int TerminationId { get; set; }
        public string TenantName { get; set; } = string.Empty;
        public string RoomNumber { get; set; } = string.Empty;
        public DateTime RequestedDate { get; set; }
        public string Reason { get; set; } = string.Empty;

        [Display(Name = "Approval Notes / Inspection Remarks")]
        public string ApprovalNotes { get; set; } = string.Empty;
    }
}
