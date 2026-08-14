using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class RentalTerminationApproval
{
    [Key]
    public int Id { get; set; }

    public int RentalTerminationId { get; set; }

    public int TerminationRequestStatusId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ActionDate { get; set; }

    public int ActionBy { get; set; }

    [StringLength(150)]
    public string? Remark { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("ActionBy")]
    [InverseProperty("RentalTerminationApprovals")]
    public virtual User ActionByNavigation { get; set; } = null!;

    [ForeignKey("RentalTerminationId")]
    [InverseProperty("RentalTerminationApprovals")]
    public virtual RentalAgreementTermination RentalTermination { get; set; } = null!;

    [ForeignKey("TerminationRequestStatusId")]
    [InverseProperty("RentalTerminationApprovals")]
    public virtual TerminationRequestStatus TerminationRequestStatus { get; set; } = null!;
}
