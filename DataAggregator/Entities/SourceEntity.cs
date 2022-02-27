using System.ComponentModel.DataAnnotations;

namespace DataAggregator.Entities;

public class SourceEntity
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    [Url]
    public string Url  { get; set; }
}
