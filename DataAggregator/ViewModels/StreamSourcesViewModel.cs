using Microsoft.AspNetCore.Mvc.Rendering;
using DataAggregator.Entities;

namespace DataAggregator.ViewModels;

public class StreamSourcesViewModel
{
    public string StreamName { get; set; }

    public SelectList StreamSourcesList { get; set; }

    public SelectList OtherSourcesList { get; set; }

    public StreamSourcesViewModel(string streamName, List<SourceEntity> streamSouces, List<SourceEntity> sources)
    {
        StreamName = streamName;
        StreamSourcesList = new SelectList(streamSouces, "Id", "Name");
        OtherSourcesList = new SelectList(sources.Except(streamSouces), "Id", "Name");
    }
}
