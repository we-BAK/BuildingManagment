using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class User
{
    [Key]
    public int Id { get; set; }

    public int SexId { get; set; }

    [StringLength(30)]
    public string FirstName { get; set; } = null!;

    [StringLength(30)]
    public string MiddleName { get; set; } = null!;

    [StringLength(30)]
    public string? LastName { get; set; }

    [StringLength(61)]
    public string FullName { get; set; } = null!;

    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(350)]
    public string Password { get; set; } = null!;

    [StringLength(50)]
    public string UserName { get; set; } = null!;

    [StringLength(50)]
    public string PhoneNumber { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastLogon { get; set; }

    public int? FailureCount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? BlockEndDate { get; set; }

    public int DefaultLanguageId { get; set; }

    [Column("Use2FA")]
    public bool Use2Fa { get; set; }

    [Column("TelegramUID")]
    [StringLength(30)]
    public string? TelegramUid { get; set; }

    public string? Photo { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<BuildingRequest> BuildingRequests { get; set; } = new List<BuildingRequest>();

    [InverseProperty("User")]
    public virtual ICollection<Building> Buildings { get; set; } = new List<Building>();

    [InverseProperty("Receiver")]
    public virtual ICollection<Chat> ChatReceivers { get; set; } = new List<Chat>();

    [InverseProperty("Sender")]
    public virtual ICollection<Chat> ChatSenders { get; set; } = new List<Chat>();

    [ForeignKey("DefaultLanguageId")]
    [InverseProperty("Users")]
    public virtual Language DefaultLanguage { get; set; } = null!;

    [InverseProperty("User")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    [InverseProperty("User")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    [InverseProperty("User")]
    public virtual ICollection<ItemCategory> ItemCategories { get; set; } = new List<ItemCategory>();

    [InverseProperty("User")]
    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    [InverseProperty("AprovedByNavigation")]
    public virtual ICollection<MaintenanceRequestAllocation> MaintenanceRequestAllocations { get; set; } = new List<MaintenanceRequestAllocation>();

    [InverseProperty("User")]
    public virtual ICollection<MaintenanceRequest> MaintenanceRequests { get; set; } = new List<MaintenanceRequest>();

    [InverseProperty("User")]
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    [InverseProperty("User")]
    public virtual ICollection<OrganizationUser> OrganizationUsers { get; set; } = new List<OrganizationUser>();

    [InverseProperty("ActionByNavigation")]
    public virtual ICollection<RentalTerminationApproval> RentalTerminationApprovals { get; set; } = new List<RentalTerminationApproval>();

    [InverseProperty("AccceptedByNavigation")]
    public virtual ICollection<RoomRentalPaymentDetail> RoomRentalPaymentDetails { get; set; } = new List<RoomRentalPaymentDetail>();

    [InverseProperty("User")]
    public virtual ICollection<RoomRentalRequest> RoomRentalRequests { get; set; } = new List<RoomRentalRequest>();

    [InverseProperty("User")]
    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();

    [ForeignKey("SexId")]
    [InverseProperty("Users")]
    public virtual Sex Sex { get; set; } = null!;

    [InverseProperty("User")]
    public virtual ICollection<ShopRequest> ShopRequests { get; set; } = new List<ShopRequest>();

    [InverseProperty("User")]
    public virtual ICollection<Shop> Shops { get; set; } = new List<Shop>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<TenantUser> TenantUserCreatedByNavigations { get; set; } = new List<TenantUser>();

    [InverseProperty("User")]
    public virtual ICollection<TenantUser> TenantUserUsers { get; set; } = new List<TenantUser>();

    [InverseProperty("User")]
    public virtual ICollection<UserEmail> UserEmails { get; set; } = new List<UserEmail>();

    [InverseProperty("User")]
    public virtual ICollection<UserImage> UserImages { get; set; } = new List<UserImage>();

    [InverseProperty("User")]
    public virtual ICollection<UserLogon> UserLogons { get; set; } = new List<UserLogon>();

    [InverseProperty("User")]
    public virtual ICollection<UserPasswordReset> UserPasswordResets { get; set; } = new List<UserPasswordReset>();

    [InverseProperty("User")]
    public virtual ICollection<UserPassword> UserPasswords { get; set; } = new List<UserPassword>();

    [InverseProperty("User")]
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    [InverseProperty("ActionByNavigation")]
    public virtual ICollection<Withdrawl> Withdrawls { get; set; } = new List<Withdrawl>();
}
