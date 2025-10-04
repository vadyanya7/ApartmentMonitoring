namespace ApartmentMonitoring.Infrastructure;

public partial class WhatsappMessage
{
    public Guid Id { get; set; }

    public string? MessageContent { get; set; }

    public string SenderPhone { get; set; } = null!;

    public string SenderId { get; set; } = null!;

    public string? MediaUrl { get; set; }

    public string? GroupName { get; set; }

    public string? GroupId { get; set; }

    public DateTime MessageTimestamp { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? ContentHash { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
