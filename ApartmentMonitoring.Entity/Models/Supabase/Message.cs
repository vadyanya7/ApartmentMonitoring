using System;
using System.Collections.Generic;

namespace ApartmentMonitoring.Infrastructure;

public partial class Message
{
    public Guid Id { get; set; }

    public Guid? ConversationId { get; set; }

    public Guid? SenderId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? ReadAt { get; set; }

    public virtual Conversation? Conversation { get; set; }

    public virtual User? Sender { get; set; }

    public virtual User1? SenderNavigation { get; set; }
}
