using System;
using System.Collections.Generic;

namespace ApartmentMonitoring.Infrastructure.Models.Supabase;

public partial class SchemaMigration2
{
    public string Version { get; set; } = null!;

    public List<string>? Statements { get; set; }

    public string? Name { get; set; }
}
