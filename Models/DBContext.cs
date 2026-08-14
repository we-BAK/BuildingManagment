using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class DBContext : DbContext
{
    public DBContext()
    {
    }

    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<Building> Buildings { get; set; }

    public virtual DbSet<BuildingEmployee> BuildingEmployees { get; set; }

    public virtual DbSet<BuildingImage> BuildingImages { get; set; }

    public virtual DbSet<BuildingRequest> BuildingRequests { get; set; }

    public virtual DbSet<BuildingRequestStatus> BuildingRequestStatuses { get; set; }

    public virtual DbSet<BuildingSpecification> BuildingSpecifications { get; set; }

    public virtual DbSet<BuildingType> BuildingTypes { get; set; }

    public virtual DbSet<BusinessArea> BusinessAreas { get; set; }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<ChatStatus> ChatStatuses { get; set; }

    public virtual DbSet<ChatVersion> ChatVersions { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Documente> Documentes { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeType> EmployeeTypes { get; set; }

    public virtual DbSet<Floor> Floors { get; set; }

    public virtual DbSet<FloorPrice> FloorPrices { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceStatus> InvoiceStatuses { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<ItemCategory> ItemCategories { get; set; }

    public virtual DbSet<ItemEntry> ItemEntries { get; set; }

    public virtual DbSet<ItemEntryVaration> ItemEntryVarations { get; set; }

    public virtual DbSet<ItemImage> ItemImages { get; set; }

    public virtual DbSet<ItemPrice> ItemPrices { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<MaintenanceAllocationStatus> MaintenanceAllocationStatuses { get; set; }

    public virtual DbSet<MaintenanceRequest> MaintenanceRequests { get; set; }

    public virtual DbSet<MaintenanceRequestAllocation> MaintenanceRequestAllocations { get; set; }

    public virtual DbSet<MaintenanceStatus> MaintenanceStatuses { get; set; }

    public virtual DbSet<MaintenanceStatusReport> MaintenanceStatusReports { get; set; }

    public virtual DbSet<MaintenanceType> MaintenanceTypes { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<MenuCategory> MenuCategories { get; set; }

    public virtual DbSet<MenuType> MenuTypes { get; set; }

    public virtual DbSet<Month> Months { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<NotificationStatus> NotificationStatuses { get; set; }

    public virtual DbSet<NotificationType> NotificationTypes { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<OrganizationType> OrganizationTypes { get; set; }

    public virtual DbSet<OrganizationUser> OrganizationUsers { get; set; }

    public virtual DbSet<PasswordResetCode> PasswordResetCodes { get; set; }

    public virtual DbSet<PaymentMode> PaymentModes { get; set; }

    public virtual DbSet<PaymentType> PaymentTypes { get; set; }

    public virtual DbSet<RentalAgreementTermination> RentalAgreementTerminations { get; set; }

    public virtual DbSet<RentalTerminationApproval> RentalTerminationApprovals { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleApplication> RoleApplications { get; set; }

    public virtual DbSet<RolesMenu> RolesMenus { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomPrice> RoomPrices { get; set; }

    public virtual DbSet<RoomProperty> RoomProperties { get; set; }

    public virtual DbSet<RoomPropertyType> RoomPropertyTypes { get; set; }

    public virtual DbSet<RoomPropertyTypeOption> RoomPropertyTypeOptions { get; set; }

    public virtual DbSet<RoomRental> RoomRentals { get; set; }

    public virtual DbSet<RoomRentalPayment> RoomRentalPayments { get; set; }

    public virtual DbSet<RoomRentalPaymentDetail> RoomRentalPaymentDetails { get; set; }

    public virtual DbSet<RoomRentalRequest> RoomRentalRequests { get; set; }

    public virtual DbSet<RoomRequestStatus> RoomRequestStatuses { get; set; }

    public virtual DbSet<RoomStatue> RoomStatues { get; set; }

    public virtual DbSet<Sex> Sexes { get; set; }

    public virtual DbSet<Shop> Shops { get; set; }

    public virtual DbSet<ShopImage> ShopImages { get; set; }

    public virtual DbSet<ShopItem> ShopItems { get; set; }

    public virtual DbSet<ShopLocation> ShopLocations { get; set; }

    public virtual DbSet<ShopRequest> ShopRequests { get; set; }

    public virtual DbSet<ShopRequestStatus> ShopRequestStatuses { get; set; }

    public virtual DbSet<ShopSpecification> ShopSpecifications { get; set; }

    public virtual DbSet<ShortcutMenu> ShortcutMenus { get; set; }

    public virtual DbSet<Tenant> Tenants { get; set; }

    public virtual DbSet<TenantType> TenantTypes { get; set; }

    public virtual DbSet<TenantUser> TenantUsers { get; set; }

    public virtual DbSet<TerminationRequestStatus> TerminationRequestStatuses { get; set; }

    public virtual DbSet<UseType> UseTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserEmail> UserEmails { get; set; }

    public virtual DbSet<UserImage> UserImages { get; set; }

    public virtual DbSet<UserLogon> UserLogons { get; set; }

    public virtual DbSet<UserPassword> UserPasswords { get; set; }

    public virtual DbSet<UserPasswordReset> UserPasswordResets { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<Varation> Varations { get; set; }

    public virtual DbSet<VarationType> VarationTypes { get; set; }

    public virtual DbSet<Withdrawl> Withdrawls { get; set; }

    public virtual DbSet<Year> Years { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=DBConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Systems");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Building>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.BuildingType).WithMany(p => p.Buildings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Buildings_BuildingTypes");

            entity.HasOne(d => d.City).WithMany(p => p.Buildings).HasConstraintName("FK_Buildings_Cities");

            entity.HasOne(d => d.Location).WithMany(p => p.Buildings).HasConstraintName("FK_Buildings_Locations");

            entity.HasOne(d => d.Organization).WithMany(p => p.Buildings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Buildings_Organizations");

            entity.HasOne(d => d.UseType).WithMany(p => p.Buildings).HasConstraintName("FK_Buildings_UseTypes");

            entity.HasOne(d => d.User).WithMany(p => p.Buildings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Buildings_Users");
        });

        modelBuilder.Entity<BuildingEmployee>(entity =>
        {
            entity.Property(e => e.EmployeeId).HasDefaultValue(1);
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Building).WithMany(p => p.BuildingEmployees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BuildingEmployees_Buildings");

            entity.HasOne(d => d.Employee).WithMany(p => p.BuildingEmployees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BuildingEmployees_Employees");

            entity.HasOne(d => d.EmployeeType).WithMany(p => p.BuildingEmployees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BuildingEmployees_ServiceCategories");
        });

        modelBuilder.Entity<BuildingImage>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Building).WithMany(p => p.BuildingImages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BuildingImages_Buildings");
        });

        modelBuilder.Entity<BuildingRequest>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.BuildingType).WithMany(p => p.BuildingRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BuildingRequests_BuildingTypes");

            entity.HasOne(d => d.City).WithMany(p => p.BuildingRequests).HasConstraintName("FK_BuildingRequests_Cities");

            entity.HasOne(d => d.Location).WithMany(p => p.BuildingRequests).HasConstraintName("FK_BuildingRequests_Locations");

            entity.HasOne(d => d.Organization).WithMany(p => p.BuildingRequests).HasConstraintName("FK_BuildingRequests_Organizations");

            entity.HasOne(d => d.RequestStatus).WithMany(p => p.BuildingRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BuildingRequests_BuildingRequestStatuses");

            entity.HasOne(d => d.User).WithMany(p => p.BuildingRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BuildingRequests_Users");
        });

        modelBuilder.Entity<BuildingRequestStatus>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<BuildingSpecification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Buildingspecifications");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<BuildingType>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<BusinessArea>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.ChatStatus).WithMany(p => p.Chats)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Chats_ChatStatuses");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Chats_Chats");

            entity.HasOne(d => d.Receiver).WithMany(p => p.ChatReceivers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Chats_Users1");

            entity.HasOne(d => d.Sender).WithMany(p => p.ChatSenders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Chats_Users");
        });

        modelBuilder.Entity<ChatStatus>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<ChatVersion>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Chat).WithMany(p => p.ChatVersions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChatVersions_Chats");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Documente>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Organization).WithMany(p => p.Employees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employees_Organizations");

            entity.HasOne(d => d.Sex).WithMany(p => p.Employees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employees_Sexes");

            entity.HasOne(d => d.User).WithMany(p => p.Employees).HasConstraintName("FK_Employees_Users");
        });

        modelBuilder.Entity<EmployeeType>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Floor>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Building).WithMany(p => p.Floors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Floors_Buildings");
        });

        modelBuilder.Entity<FloorPrice>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Floor).WithMany(p => p.FloorPrices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FloorPrices_Floors");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.InvoiceStatus).WithMany(p => p.Invoices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoices_InvoiceStatuses");

            entity.HasOne(d => d.User).WithMany(p => p.Invoices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoices_Users");
        });

        modelBuilder.Entity<InvoiceStatus>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.ItemCategory).WithMany(p => p.Items)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Items_ItemCategories");

            entity.HasOne(d => d.User).WithMany(p => p.Items)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Items_Users");
        });

        modelBuilder.Entity<ItemCategory>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.User).WithMany(p => p.ItemCategories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ItemCategories_Users");
        });

        modelBuilder.Entity<ItemEntry>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.ShopItem).WithMany(p => p.ItemEntries)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ItemEntries_ShopItems");
        });

        modelBuilder.Entity<ItemEntryVaration>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.ItemEntry).WithMany(p => p.ItemEntryVarations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ItemEntryVarations_ItemEntries");

            entity.HasOne(d => d.ShopItem).WithMany(p => p.ItemEntryVarations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ItemEntryVarations_ShopItems");

            entity.HasOne(d => d.Varation).WithMany(p => p.ItemEntryVarations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ItemEntryVarations_Varations");

            entity.HasOne(d => d.VarationType).WithMany(p => p.ItemEntryVarations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ItemEntryVarations_VarationTypes");
        });

        modelBuilder.Entity<ItemImage>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Item).WithMany(p => p.ItemImages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ItemImages_Items");
        });

        modelBuilder.Entity<ItemPrice>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Item).WithMany(p => p.ItemPrices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ItemPrices_Items");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.City).WithMany(p => p.Locations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Locations_Cities");
        });

        modelBuilder.Entity<MaintenanceAllocationStatus>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<MaintenanceRequest>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.MaintenanceStatus).WithMany(p => p.MaintenanceRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MaintenanceRequests_MaintenanceStatuses");

            entity.HasOne(d => d.MaintenanceType).WithMany(p => p.MaintenanceRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MaintenanceRequests_MaintenanceTypes");

            entity.HasOne(d => d.Room).WithMany(p => p.MaintenanceRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MaintenanceRequests_Rooms");

            entity.HasOne(d => d.User).WithMany(p => p.MaintenanceRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MaintenanceRequests_Users");
        });

        modelBuilder.Entity<MaintenanceRequestAllocation>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.AllocationStatus).WithMany(p => p.MaintenanceRequestAllocations).HasConstraintName("FK_MaintenanceRequestAllocations_MaintenanceAllocationStatuses");

            entity.HasOne(d => d.AprovedByNavigation).WithMany(p => p.MaintenanceRequestAllocations).HasConstraintName("FK_MaintenanceRequestAllocations_Users");

            entity.HasOne(d => d.BuildingEmployee).WithMany(p => p.MaintenanceRequestAllocations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MaintenanceRequestAllocations_BuildingEmployees");

            entity.HasOne(d => d.MaintenanceRequest).WithMany(p => p.MaintenanceRequestAllocations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MaintenanceRequestAllocations_MaintenanceRequests");
        });

        modelBuilder.Entity<MaintenanceStatus>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<MaintenanceStatusReport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_MaintenanceStatusReport");

            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.MaintenanceRequestAllocation).WithMany(p => p.MaintenanceStatusReports)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MaintenanceStatusReport_MaintenanceRequestAllocations");
        });

        modelBuilder.Entity<MaintenanceType>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.MenuCategory).WithMany(p => p.Menus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Menus_MenuCategory");
        });

        modelBuilder.Entity<MenuCategory>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Application).WithMany(p => p.MenuCategories).HasConstraintName("FK_MenuCategory_Applications");

            entity.HasOne(d => d.MenuType).WithMany(p => p.MenuCategories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MenuCategory_MenuTypes");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent).HasConstraintName("FK_MenuCategory_MenuCategory");
        });

        modelBuilder.Entity<MenuType>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Month>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.NotificationStatus).WithMany(p => p.Notifications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notifications_NotificationStatuses");

            entity.HasOne(d => d.NotificationType).WithMany(p => p.Notifications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notifications_NotificationTypes");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notifications_Users");
        });

        modelBuilder.Entity<NotificationStatus>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<NotificationType>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Document).WithMany(p => p.Organizations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Organizations_Documentes");

            entity.HasOne(d => d.OrganizationType).WithMany(p => p.Organizations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Organizations_OrganizationTypes");
        });

        modelBuilder.Entity<OrganizationType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_OwnershipTypes");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<OrganizationUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_OwnerUsers");

            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Organization).WithMany(p => p.OrganizationUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OwnerUsers_Owners");

            entity.HasOne(d => d.User).WithMany(p => p.OrganizationUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OwnerUsers_Users");
        });

        modelBuilder.Entity<PasswordResetCode>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<PaymentMode>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<RentalAgreementTermination>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Document).WithMany(p => p.RentalAgreementTerminations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RentalAgreementTerminations_Documentes");

            entity.HasOne(d => d.RoomRental).WithMany(p => p.RentalAgreementTerminations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RentalAgreementTerminations_RoomRentals");
        });

        modelBuilder.Entity<RentalTerminationApproval>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.ActionByNavigation).WithMany(p => p.RentalTerminationApprovals)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RentalTerminationApprovals_Users");

            entity.HasOne(d => d.RentalTermination).WithMany(p => p.RentalTerminationApprovals)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RentalTerminationApprovals_RentalAgreementTerminations");

            entity.HasOne(d => d.TerminationRequestStatus).WithMany(p => p.RentalTerminationApprovals)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RentalTerminationApprovals_TerminationRequestStatuses");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<RoleApplication>(entity =>
        {
            entity.ToView("RoleApplications");
        });

        modelBuilder.Entity<RolesMenu>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Menu).WithMany(p => p.RolesMenus).HasConstraintName("FK_RolesMenus_Menus");

            entity.HasOne(d => d.Role).WithMany(p => p.RolesMenus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolesMenus_Roles");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Floor).WithMany(p => p.Rooms)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rooms_Floors");

            entity.HasOne(d => d.RoomStatus).WithMany(p => p.Rooms)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rooms_RoomStatues");

            entity.HasOne(d => d.User).WithMany(p => p.Rooms)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rooms_Users");
        });

        modelBuilder.Entity<RoomPrice>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Room).WithMany(p => p.RoomPrices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoomPrices_Rooms");
        });

        modelBuilder.Entity<RoomProperty>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Room).WithMany(p => p.RoomProperties)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoomProperties_Rooms");

            entity.HasOne(d => d.RoomPropertyType).WithMany(p => p.RoomProperties)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoomProperties_RoomPropertyTypes");
        });

        modelBuilder.Entity<RoomPropertyType>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<RoomPropertyTypeOption>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.RoomPropertyType).WithMany(p => p.RoomPropertyTypeOptions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoomPropertyTypeOptions_RoomPropertyTypes");
        });

        modelBuilder.Entity<RoomRental>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.BusinessArea).WithMany(p => p.RoomRentals)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoomRentals_BusinessAreas");

            entity.HasOne(d => d.Document).WithMany(p => p.RoomRentals).HasConstraintName("FK_RoomRentals_Documentes");

            entity.HasOne(d => d.Room).WithMany(p => p.RoomRentals)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoomRentals_Rooms");

            entity.HasOne(d => d.Tenant).WithMany(p => p.RoomRentals)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoomRentals_Tenants");
        });

        modelBuilder.Entity<RoomRentalPayment>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.PaymentMode).WithMany(p => p.RoomRentalPayments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoomRentalPayments_PaymentModes");

            entity.HasOne(d => d.PaymentType).WithMany(p => p.RoomRentalPayments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoomRentalPayments_PaymentTypes");

            entity.HasOne(d => d.RoomRental).WithMany(p => p.RoomRentalPayments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoomRentalPayments_RoomRentals");
        });

        modelBuilder.Entity<RoomRentalPaymentDetail>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.AccceptedByNavigation).WithMany(p => p.RoomRentalPaymentDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoomRentalPaymentDetails_Users");

            entity.HasOne(d => d.Month).WithMany(p => p.RoomRentalPaymentDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoomRentalPaymentDetails_Months");

            entity.HasOne(d => d.RoomRentalPayment).WithMany(p => p.RoomRentalPaymentDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoomRentalPaymentDetails_RoomRentalPayments");

            entity.HasOne(d => d.Year).WithMany(p => p.RoomRentalPaymentDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoomRentalPaymentDetails_Years");
        });

        modelBuilder.Entity<RoomRentalRequest>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.RequestStatus).WithMany(p => p.RoomRentalRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoomRentalRequests_RoomRequestStatuses");

            entity.HasOne(d => d.Room).WithMany(p => p.RoomRentalRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoomRentalRequests_Rooms");

            entity.HasOne(d => d.User).WithMany(p => p.RoomRentalRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoomRentalRequests_Users");
        });

        modelBuilder.Entity<RoomRequestStatus>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<RoomStatue>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Sex>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Shop>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.BusinessArea).WithMany(p => p.Shops)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shops_BusinessAreas");

            entity.HasOne(d => d.User).WithMany(p => p.Shops)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shops_Users");
        });

        modelBuilder.Entity<ShopImage>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Shop).WithMany(p => p.ShopImages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ShopImages_Shops");
        });

        modelBuilder.Entity<ShopItem>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Item).WithMany(p => p.ShopItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ShopItems_Items");

            entity.HasOne(d => d.Shop).WithMany(p => p.ShopItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ShopItems_Shops");
        });

        modelBuilder.Entity<ShopLocation>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Room).WithMany(p => p.ShopLocations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ShopLocations_Rooms");

            entity.HasOne(d => d.Shop).WithMany(p => p.ShopLocations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ShopLocations_Shops");
        });

        modelBuilder.Entity<ShopRequest>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.RequestStatus).WithMany(p => p.ShopRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ShopRequests_ShopRequestStatuses");

            entity.HasOne(d => d.User).WithMany(p => p.ShopRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ShopRequests_Users");
        });

        modelBuilder.Entity<ShopRequestStatus>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<ShopSpecification>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.ShopRequest).WithMany(p => p.ShopSpecifications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ShopSpecifications_ShopRequests");

            entity.HasOne(d => d.UseType).WithMany(p => p.ShopSpecifications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ShopSpecifications_UseTypes");
        });

        modelBuilder.Entity<ShortcutMenu>(entity =>
        {
            entity.HasOne(d => d.Application).WithMany(p => p.ShortcutMenus).HasConstraintName("FK_ShortcutMenus_Applications");

            entity.HasOne(d => d.Menu).WithMany(p => p.ShortcutMenus).HasConstraintName("FK_ShortcutMenus_Menus");
        });

        modelBuilder.Entity<Tenant>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Building).WithMany(p => p.Tenants)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tenants_Buildings");

            entity.HasOne(d => d.TenantType).WithMany(p => p.Tenants)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tenants_TenantTypes");
        });

        modelBuilder.Entity<TenantType>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<TenantUser>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TenantUserCreatedByNavigations).HasConstraintName("FK_TenantUsers_Users1");

            entity.HasOne(d => d.Tenant).WithMany(p => p.TenantUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TenantUsers_Tenants");

            entity.HasOne(d => d.User).WithMany(p => p.TenantUserUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TenantUsers_Users");
        });

        modelBuilder.Entity<TerminationRequestStatus>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<UseType>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.DefaultLanguageId).HasDefaultValue(1);
            entity.Property(e => e.FailureCount).HasDefaultValue(0);
            entity.Property(e => e.FullName).HasComputedColumnSql("(([FirstName]+' ')+[MiddleName])", false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.SexId).HasDefaultValue(1);
            entity.Property(e => e.Use2Fa).HasDefaultValue(true);

            entity.HasOne(d => d.DefaultLanguage).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Languages");

            entity.HasOne(d => d.Sex).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Sexes");
        });

        modelBuilder.Entity<UserEmail>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.User).WithMany(p => p.UserEmails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserEmails_Users");
        });

        modelBuilder.Entity<UserImage>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.User).WithMany(p => p.UserImages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserImages_Users");
        });

        modelBuilder.Entity<UserLogon>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.User).WithMany(p => p.UserLogons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserLogons_Users");
        });

        modelBuilder.Entity<UserPassword>(entity =>
        {
            entity.HasOne(d => d.User).WithMany(p => p.UserPasswords)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserPasswords_Users");
        });

        modelBuilder.Entity<UserPasswordReset>(entity =>
        {
            entity.HasOne(d => d.User).WithMany(p => p.UserPasswordResets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserPasswordResets_Users");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoles_Roles");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoles_Users");
        });

        modelBuilder.Entity<Varation>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.VarationType).WithMany(p => p.Varations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Varations_VarationTypes");
        });

        modelBuilder.Entity<VarationType>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Item).WithMany(p => p.VarationTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VarationTypes_Items");
        });

        modelBuilder.Entity<Withdrawl>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.ActionByNavigation).WithMany(p => p.Withdrawls)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Withdrawls_Users");

            entity.HasOne(d => d.Entry).WithMany(p => p.Withdrawls)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Withdrawls_ItemEntries");
        });

        modelBuilder.Entity<Year>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
