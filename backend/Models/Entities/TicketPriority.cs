using System;
using System.Collections.Generic;

namespace backend.Models.Entities;

public partial class TicketPriority
{
    public int priorityId { get; set; }

    public string? priority { get; set; }

    public byte[]? remarks { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
