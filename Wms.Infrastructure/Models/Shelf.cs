using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Wms.Infrastructure.Models.Other;

namespace Wms.Infrastructure.Models;

public partial class Shelf : ICreationTrackable
{
    public int Id { get; set; }

    public string ShelfName { get; set; } = null!;

    public int AreaId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Area Area { get; set; } = null!;

    //[JsonIgnore]
    public virtual ICollection<StorageLocation> StorageLocations { get; set; } = new List<StorageLocation>();
}


public partial class ShelfDto
{
    public int Id { get; set; }

    [Required]
    public string ShelfName { get; set; } = null!;
    public int AreaId { get; set; }
}
