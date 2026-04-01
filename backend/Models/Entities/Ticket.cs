using System;
using System.Collections.Generic;

namespace backend.Models.Entities;

public partial class Ticket
{
    public int ticketId { get; set; }

    public string? title { get; set; }

    public string? description { get; set; }

    public int branchId { get; set; }

    public int departmentId { get; set; }

    public int systemId { get; set; }

    public int statusId { get; set; }

    public int? priorityId { get; set; }

    public bool? isActive { get; set; }

    public string? createdBy { get; set; }

    public DateTime? createdAt { get; set; }

    public string? updatedBy { get; set; }

    public DateTime? updatedAt { get; set; }

    public virtual ICollection<TicketStatusChange> TicketStatusChanges { get; set; } = new List<TicketStatusChange>();

    public virtual Branch branch { get; set; } = null!;

    public virtual Department department { get; set; } = null!;

    public virtual TicketPriority? priority { get; set; }

    public virtual TicketStatus status { get; set; } = null!;

    public virtual SystemEntity system { get; set; } = null!;
}
