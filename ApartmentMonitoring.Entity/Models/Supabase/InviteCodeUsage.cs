using System;
using System.Collections.Generic;

namespace ApartmentMonitoring.Infrastructure;

public partial class InviteCodeUsage
{
    public Guid Id { get; set; }

    public Guid? InviteCodeId { get; set; }

    public Guid? UsedById { get; set; }

    public DateTime? UsedAt { get; set; }

    public virtual InviteCode? InviteCode { get; set; }

    public virtual User1? UsedBy { get; set; }
}
