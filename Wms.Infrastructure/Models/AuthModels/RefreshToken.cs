namespace Wms.Infrastructure.Models.AuthModels;

public record RefreshToken
{
    public int Id { get; init; }
    public int? ParentId { get; init; }
    public string Username { get; set; }
    public string Token { get; set; } = default!;
    public DateTime? UsedAt { get; set; }
    public DateTime? RevokedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
}
