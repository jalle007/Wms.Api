using System.Text.Json.Serialization;
using Wms.Infrastructure.Models.Other;

namespace Wms.Infrastructure.Models;

public partial class VmsConfig : ICreationTrackable
{
    public int Id { get; set; }

    public int LocationId { get; set; }

    public int WarehouseId { get; set; }

    public int AreaId { get; set; }

    public DateTime CreatedAt { get; set; }

    [JsonIgnore]
    public virtual Area Area { get; set; } = null!;

    [JsonIgnore]
    public virtual Location Location { get; set; } = null!;

    [JsonIgnore]
    public virtual Warehouse Warehouse { get; set; } = null!;
}


public partial class VmsConfigDto
{
    public int Id { get; set; }

    public int LocationId { get; set; }

    public int WarehouseId { get; set; }

    public int AreaId { get; set; }

}
