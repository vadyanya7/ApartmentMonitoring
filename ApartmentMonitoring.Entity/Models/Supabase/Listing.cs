using System;
using System.Collections.Generic;

namespace ApartmentMonitoring.Infrastructure;

public partial class Listing
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public string ProjectName { get; set; } = null!;

    public string PropertyType { get; set; } = null!;

    public string Bedrooms { get; set; } = null!;

    public int Size { get; set; }

    public string SizeUnit { get; set; } = null!;

    public long Price { get; set; }

    public string? Floor { get; set; }

    public string? View { get; set; }

    public bool? IsCovered { get; set; }

    public string? AdditionalInfo { get; set; }

    public List<string>? Photos { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Phone { get; set; }

    /// <summary>
    /// True, если это distress/resale below OP/urgent sale и т.п.
    /// </summary>
    public bool IsDistress { get; set; }

    /// <summary>
    /// Original Price (OP) в AED, целое число
    /// </summary>
    public long? OpPrice { get; set; }

    /// <summary>
    /// Selling/Asking Price (SP) в AED, целое число
    /// </summary>
    public long? SpPrice { get; set; }

    /// <summary>
    /// Скидка от OP в %, округлено до 0.01. Формула: (OP - SP)/OP*100
    /// </summary>
    public decimal? DiscountFromOpPercent { get; set; }

    public virtual User1? User { get; set; }
}
