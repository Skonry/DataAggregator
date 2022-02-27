using System.ComponentModel.DataAnnotations;

namespace DataAggregator.Entities;

public class StreamEntity
{
    public int Id { get; set; }


    [Required]
    public string Name { get; set; }

    public ICollection<SourceEntity> Sources { get; set; }

    public ICollection<FilterEntity> Filters { get; set; }
}
