using System;
using System.Collections.Generic;

namespace backend.Models.Entities;

public partial class SystemDeveloperAssign
{
    public int sysDevId { get; set; }

    public int? systemId { get; set; }

    public int? userId { get; set; }

    public virtual SystemEntity? system { get; set; }
}
