using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BMS.Models;

public partial class Chat
{
    [Key]
    public int Id { get; set; }

    public int SenderId { get; set; }

    public int ReceiverId { get; set; }

    public int ParentId { get; set; }

    [StringLength(150)]
    public string Message { get; set; } = null!;

    public int ChatStatusId { get; set; }

    public DateOnly Date { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    [ForeignKey("ChatStatusId")]
    [InverseProperty("Chats")]
    public virtual ChatStatus ChatStatus { get; set; } = null!;

    [InverseProperty("Chat")]
    public virtual ICollection<ChatVersion> ChatVersions { get; set; } = new List<ChatVersion>();

    [InverseProperty("Parent")]
    public virtual ICollection<Chat> InverseParent { get; set; } = new List<Chat>();

    [ForeignKey("ParentId")]
    [InverseProperty("InverseParent")]
    public virtual Chat Parent { get; set; } = null!;

    [ForeignKey("ReceiverId")]
    [InverseProperty("ChatReceivers")]
    public virtual User Receiver { get; set; } = null!;

    [ForeignKey("SenderId")]
    [InverseProperty("ChatSenders")]
    public virtual User Sender { get; set; } = null!;
}
