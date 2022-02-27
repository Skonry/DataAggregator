using System.ComponentModel.DataAnnotations;
using DataAggregator.Common;

namespace DataAggregator.Entities; 

public class FilterEntity
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public FilterType Type { get; set; }

    [Required]
    public string Value { get; set; }
}
