using System.ComponentModel.DataAnnotations;
using Wms.Infrastructure.Models.Other;
using static Wms.Infrastructure.Enums;

namespace Wms.Infrastructure.Models;

public partial class Order : ICreationTrackable
{
    public int Id { get; set; }

    public int Status { get; set; }

    public int Type { get; set; }

    public int SourceId { get; set; }

    public int SampleId { get; set; }

    public int TargetId { get; set; }

    public string? Item { get; set; }

    public ItemType ItemType { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<OrderTrace> OrderTraces { get; set; } = new List<OrderTrace>();

    public virtual Sample Sample { get; set; } = null!;

    public virtual StorageLocation Source { get; set; } = null!;

    public virtual StorageLocation Target { get; set; } = null!;
}


public partial class OrderDto
{
    public int Id { get; set; }

    public int Status { get; set; }

    public int Type { get; set; }

    public int SourceId { get; set; }

    public int SampleId { get; set; }

    public int TargetId { get; set; }

    [Required]
    public string? Item { get; set; }


}
