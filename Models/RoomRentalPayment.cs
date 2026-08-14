using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class RoomRentalPayment
{
    [Key]
    public int Id { get; set; }

    public int RoomRentalId { get; set; }

    public int PaymentTypeId { get; set; }

    public int PaymentModeId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime PaidDate { get; set; }

    public double TotalAmount { get; set; }

    [StringLength(50)]
    public string InvoiceNumber { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("PaymentModeId")]
    [InverseProperty("RoomRentalPayments")]
    public virtual PaymentMode PaymentMode { get; set; } = null!;

    [ForeignKey("PaymentTypeId")]
    [InverseProperty("RoomRentalPayments")]
    public virtual PaymentType PaymentType { get; set; } = null!;

    [ForeignKey("RoomRentalId")]
    [InverseProperty("RoomRentalPayments")]
    public virtual RoomRental RoomRental { get; set; } = null!;

    [InverseProperty("RoomRentalPayment")]
    public virtual ICollection<RoomRentalPaymentDetail> RoomRentalPaymentDetails { get; set; } = new List<RoomRentalPaymentDetail>();
}
