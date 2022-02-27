using System.ServiceModel.Syndication;
using System.Xml;
using DataAggregator.Entities;
using DataAggregator.Common;

namespace DataAggregator.Services;

public class FeedItem
{
    public string Title { get; set; }

    public string Date { get; set; }

    public string Link { get; set; }
}

public class StreamSourceData
{
    public string SourceName { get; set; }

    public List<FeedItem> FeedItems { get; set; }

    public StreamSourceData(string sourceName, SyndicationFeed feed, ICollection<FilterEntity> filters)
    {
        SourceName = sourceName;

        FeedItems = new List<FeedItem>();

        List<SyndicationItem> filteredItems = FilterFeedItems(feed.Items, filters);

        foreach (var item in filteredItems)
        {
            var feedItem = new FeedItem()
            {
                Title = item.Title.Text,
                Date = item.PublishDate.ToString("dd/MM/yyyy"),
                Link = item.Links[0].Uri.ToString()
            };

            FeedItems.Add(feedItem);
        }
    }

    public List<SyndicationItem> FilterFeedItems(IEnumerable<SyndicationItem> feedItems, ICollection<FilterEntity> filters)
    {
        List<SyndicationItem> filteredItems = new List<SyndicationItem>();

        foreach (var item in feedItems)
        {
            if (filters.All(filter => ItemMatchFilter(item, filter)))
            {
                filteredItems.Add(item);
            }
        }

        return filteredItems;
    }

    public bool ItemMatchFilter(SyndicationItem item, FilterEntity filter)
    {
        if (filter.Type == FilterType.WithWord)
        {
            string filterValue = filter.Value.ToLower();

            List<string> texts = new List<string>();
            texts.Add(item.Title.Text.ToLower());
            texts.Add(item.Summary.Text.ToLower());

            return texts.Any(text => text.Contains(filterValue));
        }
        else if (filter.Type == FilterType.AfterDate)
        {
            return item.PublishDate.Date > DateTime.Parse(filter.Value);
        }
        else
        {
            return true;
        }
    }
}

public class DisplayStreamData
{
    public string StreamName { get; set; }

    public List<StreamSourceData> Sources { get; set; }
}

public class DisplayStreamService : IDisplayStreamService
{
    public DisplayStreamData GenerateData(StreamEntity stream)
    {
        var streamSourcesData = new List<StreamSourceData>();

        foreach (var source in stream.Sources)
        {
            var reader = XmlReader.Create(source.Url);

            var feed = SyndicationFeed.Load(reader);

            var streamSourceData = new StreamSourceData(source.Name, feed, stream.Filters);

            streamSourcesData.Add(streamSourceData);
        }

        DisplayStreamData displayStreamData = new DisplayStreamData()
        {
            StreamName = stream.Name,
            Sources = streamSourcesData,
        };

        return displayStreamData;
    }
}
