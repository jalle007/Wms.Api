using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Wms.Infrastructure.Models.Other;

namespace Wms.Infrastructure.Models;

public partial class Warehouse : ICreationTrackable
{
    public int Id { get; set; }

    public string WarehouseName { get; set; } = null!;

    public int LocationId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Area> Areas { get; set; } = new List<Area>();
    
    [JsonIgnore]
    public virtual Location Location { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<StorageLocation> StorageLocations { get; set; } = new List<StorageLocation>();
    [JsonIgnore]
    public virtual ICollection<VmsConfig> VmsConfigs { get; set; } = new List<VmsConfig>();
}

public partial class WarehouseDto
{
    public int Id { get; set; }

    [Required]
    public string WarehouseName { get; set; } = null!;
    public int LocationId { get; set; }
}