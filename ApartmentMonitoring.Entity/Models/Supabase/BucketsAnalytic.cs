using System;
using System.Collections.Generic;

namespace ApartmentMonitoring.Infrastructure;

public partial class BucketsAnalytic
{
    public string Id { get; set; } = null!;

    public string Format { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
