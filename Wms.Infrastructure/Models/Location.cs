using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Wms.Infrastructure.Models.Other;

namespace Wms.Infrastructure.Models;

public partial class Location : ICreationTrackable
{
    public int Id { get; set; }

    public string LocationName { get; set; } = null!;

    public string ShortCode { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string? Country { get; set; }

    public DateTime CreatedAt { get; set; }

    [JsonIgnore]
    public virtual ICollection<StorageLocation> StorageLocations { get; set; } = new List<StorageLocation>();
    [JsonIgnore]

    public virtual ICollection<VmsConfig> VmsConfigs { get; set; } = new List<VmsConfig>();

    public virtual ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
}

public partial class LocationDto
{
    public int Id { get; set; }

    [Required]
    public string LocationName { get; set; } = null!;

    [Required]
    public string ShortCode { get; set; } = null!;
    
    [Required]
    public string Address { get; set; } = null!;

    [Required]
    public string City { get; set; } = null!;

    [Required]
    public string Country { get; set; } = null!;

}