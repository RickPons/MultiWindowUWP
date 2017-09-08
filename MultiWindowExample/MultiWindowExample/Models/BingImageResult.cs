using System.Collections.Generic;

namespace MultiWindowExample.Models
{

    public class BingImageResult
    {
        public string _type { get; set; }
        public Instrumentation instrumentation { get; set; }
        public string webSearchUrl { get; set; }
        public int totalEstimatedMatches { get; set; }
       
        public IEnumerable<Image> value { get; set; }
        public object queryExpansions { get; set; }
        public int nextOffsetAddCount { get; set; }
        public object pivotSuggestions { get; set; }
        public bool displayShoppingSourcesBadges { get; set; }
        public bool displayRecipeSourcesBadges { get; set; }
        public object similarTerms { get; set; }
    }

    public class Instrumentation
    {
        public string pageLoadPingUrl { get; set; }
    }

    public class Image
    {
        public string name { get; set; }
        public object datePublished { get; set; }
        public object homePageUrl { get; set; }
        public string contentSize { get; set; }
        public string hostPageDisplayUrl { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public Thumbnail thumbnail { get; set; }
        public string imageInsightsToken { get; set; }
        public object insightsSourcesSummary { get; set; }
        public string imageId { get; set; }
        public string accentColor { get; set; }
        public string webSearchUrl { get; set; }
        public string thumbnailUrl { get; set; }
        public string encodingFormat { get; set; }
        public string contentUrl { get; set; }
    }

    public class Thumbnail
    {
        public int width { get; set; }
        public int height { get; set; }
    }

}
