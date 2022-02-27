using DataAggregator.Services;

namespace DataAggregator.ViewModels;

public class StreamViewModel
{
    public int StreamId { get; set; }

    public string StreamName { get; set; }

    public List<StreamSourceData> Sources { get; set; }
}
