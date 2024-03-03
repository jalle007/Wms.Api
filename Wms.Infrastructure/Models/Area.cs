using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Wms.Infrastructure.Models.Other;

namespace Wms.Infrastructure.Models;

public partial class Area : ICreationTrackable
{
    public int Id { get; set; }

    public string AreaName { get; set; } = null!;

    public int WarehouseId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Shelf> Shelves { get; set; } // = new List<Shelf>();
    
    //[JsonIgnore]
    public virtual ICollection<StorageLocation> StorageLocations { get; set; } = new List<StorageLocation>();
    
    //[JsonIgnore]
    public virtual ICollection<VmsConfig> VmsConfigs { get; set; } = new List<VmsConfig>();
    //[JsonIgnore]
    public virtual Warehouse Warehouse { get; set; } = null!;
}


public partial class AreaDto
{
    public int Id { get; set; }
    
    [Required]
    public string AreaName { get; set; } = null!;

    public int WarehouseId { get; set; }
}