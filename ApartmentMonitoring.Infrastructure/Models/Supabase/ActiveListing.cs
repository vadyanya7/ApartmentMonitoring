using System;
using System.Collections.Generic;

namespace ApartmentMonitoring.Infrastructure.Models.Supabase;

public partial class ActiveListing
{
    public Guid? Id { get; set; }

    public Guid? UserId { get; set; }

    public string? ProjectName { get; set; }

    public string? PropertyType { get; set; }

    public string? Bedrooms { get; set; }

    public int? Size { get; set; }

    public string? SizeUnit { get; set; }

    public long? Price { get; set; }

    public string? Floor { get; set; }

    public string? View { get; set; }

    public bool? IsCovered { get; set; }

    public string? AdditionalInfo { get; set; }

    public List<string>? Photos { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Phone { get; set; }
}
