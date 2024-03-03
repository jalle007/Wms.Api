using System.ComponentModel.DataAnnotations;
using Wms.Infrastructure.Models.Other;

namespace Wms.Infrastructure.Models;

public partial class Sample : ICreationTrackable
{
    public int Id { get; set; }

    public string Barcode { get; set; } = null!;

    public int ParentId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Sample> InverseParent { get; set; } = new List<Sample>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Sample Parent { get; set; } = null!;
}


public partial class SampleDto
{
    public int Id { get; set; }

    [Required]
    public string Barcode { get; set; } = null!;

    public int ParentId { get; set; }

}