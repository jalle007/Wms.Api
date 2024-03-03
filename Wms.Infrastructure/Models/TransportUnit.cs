using System.ComponentModel.DataAnnotations;
using Wms.Infrastructure.Models.Other;

namespace Wms.Infrastructure.Models;

public partial class TransportUnit : ICreationTrackable
{
    public int Id { get; set; }

    public string TransportUnitName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}

public partial class TransportUnitDto
{
    public int Id { get; set; }

    [Required]
    public string TransportUnitName { get; set; } = null!;

}
