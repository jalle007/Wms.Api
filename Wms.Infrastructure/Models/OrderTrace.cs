using System.ComponentModel.DataAnnotations;

namespace Wms.Infrastructure.Models;

public partial class OrderTrace
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public string UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}


public partial class OrderTraceDto
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    [Required]
    public string UserId { get; set; }
}
