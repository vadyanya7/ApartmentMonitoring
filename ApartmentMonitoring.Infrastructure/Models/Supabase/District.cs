using System;
using System.Collections.Generic;

namespace ApartmentMonitoring.Infrastructure.Models.Supabase;

public partial class District
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
