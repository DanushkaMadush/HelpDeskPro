using System;
using System.Collections.Generic;

namespace backend.Models.Entities;

public partial class Department
{
    public int departmentId { get; set; }

    public string departmentName { get; set; } = null!;

    public bool? isActive { get; set; }

    public string? createdBy { get; set; }

    public DateTime? createdAt { get; set; }

    public string? updatedBy { get; set; }

    public DateTime? updatedAt { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
