using System;
using System.Collections.Generic;

namespace ApartmentMonitoring.Infrastructure.Models.Supabase;

public partial class Subscription1
{
    public long Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public long MinPrice { get; set; }

    public long MaxPrice { get; set; }

    public long MinSize { get; set; }

    public long MaxSize { get; set; }

    public long Floor { get; set; }

    public long Rooms { get; set; }

    public string Districs { get; set; } = null!;

    public Guid UserId { get; set; }

    public virtual User1 User { get; set; } = null!;
}
