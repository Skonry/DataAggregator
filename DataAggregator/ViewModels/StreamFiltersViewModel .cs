using Microsoft.AspNetCore.Mvc.Rendering;
using DataAggregator.Entities;

namespace DataAggregator.ViewModels;

public class StreamFiltersViewModel
{
    public string StreamName { get; set; }

    public SelectList StreamFiltersList { get; set; }

    public SelectList OtherFiltersList { get; set; }

    public StreamFiltersViewModel(string streamName, List<FilterEntity> streamFilters, List<FilterEntity> filters)
    {
        StreamName = streamName;
        StreamFiltersList = new SelectList(streamFilters, "Id", "Name");
        OtherFiltersList = new SelectList(filters.Except(streamFilters), "Id", "Name");
    }
}
