using System;
using System.Collections.Generic;

namespace ApartmentMonitoring.Infrastructure;

public partial class InviteCode
{
    public Guid Id { get; set; }

    public string Code { get; set; } = null!;

    public Guid? OwnerId { get; set; }

    public int? MaxUses { get; set; }

    public int? CurrentUses { get; set; }

    public bool? IsUnlimited { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<InviteCodeUsage> InviteCodeUsages { get; set; } = new List<InviteCodeUsage>();

    public virtual User1? Owner { get; set; }
}
