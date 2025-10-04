using System;
using System.Collections.Generic;

namespace ApartmentMonitoring.Infrastructure.Models.Supabase;

public partial class Project
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public long? DistrictId { get; set; }

    public virtual District? District { get; set; }
}
