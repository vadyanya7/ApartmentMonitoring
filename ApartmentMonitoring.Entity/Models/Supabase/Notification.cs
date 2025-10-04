using System;
using System.Collections.Generic;

namespace ApartmentMonitoring.Infrastructure;

public partial class Notification
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Text { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string? Link { get; set; }

    public bool IsRead { get; set; }

    public DateTime CreatedAt { get; set; }
}
