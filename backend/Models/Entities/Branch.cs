using System;
using System.Collections.Generic;

namespace backend.Models.Entities;

public partial class Branch
{
    public int branchId { get; set; }

    public string branchName { get; set; } = null!;

    public string address { get; set; } = null!;

    public bool? isActive { get; set; }

    public string? createdBy { get; set; }

    public DateTime? createdAt { get; set; }

    public string updatedBy { get; set; } = null!;

    public DateTime updatedAt { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
