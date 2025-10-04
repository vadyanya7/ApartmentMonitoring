using System;
using System.Collections.Generic;

namespace ApartmentMonitoring.Infrastructure.Models.Supabase;

public partial class Messages20250922
{
    public string Topic { get; set; } = null!;

    public string Extension { get; set; } = null!;

    public string? Payload { get; set; }

    public string? Event { get; set; }

    public bool? Private { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime InsertedAt { get; set; }

    public Guid Id { get; set; }
}
