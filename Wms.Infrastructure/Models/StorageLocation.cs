using System.Text.Json.Serialization;
using Wms.Infrastructure.Models.Other;

namespace Wms.Infrastructure.Models;

public partial class StorageLocation : ICreationTrackable
{
    public int Id { get; set; }

    public int LocationId { get; set; }

    public int WarehouseId { get; set; }

    public int AreaId { get; set; }

    public int ShelfId { get; set; }

    public int Row { get; set; }

    public int Column { get; set; }

    public DateTime CreatedAt { get; set; }

    [JsonIgnore]
    public virtual Area Area { get; set; } = null!;

    [JsonIgnore]
    public virtual Location Location { get; set; } = null!;

    public virtual ICollection<Order> OrderSources { get; set; } = new List<Order>();

    public virtual ICollection<Order> OrderTargets { get; set; } = new List<Order>();

    public virtual Shelf Shelf { get; set; } = null!;

    public virtual Warehouse Warehouse { get; set; } = null!;
}


public partial class StorageLocationDto
{
    public int Id { get; set; }

    public int LocationId { get; set; }

    public int WarehouseId { get; set; }

    public int AreaId { get; set; }

    public int ShelfId { get; set; }

    public int Row { get; set; }

    public int Column { get; set; }
}