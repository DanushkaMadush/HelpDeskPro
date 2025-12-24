using System;
using System.Collections.Generic;

namespace backend.Models.Entities;

public partial class TicketStatus
{
    public int statusId { get; set; }

    public string status { get; set; } = null!;

    public byte[]? remarks { get; set; }

    public virtual ICollection<TicketStatusChange> TicketStatusChanges { get; set; } = new List<TicketStatusChange>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
