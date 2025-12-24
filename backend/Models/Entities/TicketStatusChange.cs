using System;
using System.Collections.Generic;

namespace backend.Models.Entities;

public partial class TicketStatusChange
{
    public int changeId { get; set; }

    public int? ticketId { get; set; }

    public int? statusId { get; set; }

    public int? userId { get; set; }

    public DateTime? dateTime { get; set; }

    public virtual TicketStatus? status { get; set; }

    public virtual Ticket? ticket { get; set; }
}
