using System;
using System.Collections.Generic;

namespace ApartmentMonitoring.Infrastructure;

public partial class User1
{
    public Guid Id { get; set; }

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Name { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<InviteCodeUsage> InviteCodeUsages { get; set; } = new List<InviteCodeUsage>();

    public virtual ICollection<InviteCode> InviteCodes { get; set; } = new List<InviteCode>();

    public virtual ICollection<Listing> Listings { get; set; } = new List<Listing>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
