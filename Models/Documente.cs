using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class Documente
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [InverseProperty("Document")]
    public virtual ICollection<Organization> Organizations { get; set; } = new List<Organization>();

    [InverseProperty("Document")]
    public virtual ICollection<RentalAgreementTermination> RentalAgreementTerminations { get; set; } = new List<RentalAgreementTermination>();

    [InverseProperty("Document")]
    public virtual ICollection<RoomRental> RoomRentals { get; set; } = new List<RoomRental>();
}
