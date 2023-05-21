using MyCineList.Domain.Utilities;
using Newtonsoft.Json;

namespace MyCineList.Domain.Entities
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class Movie
    {
        public Movie()
        {
            IMDBID = string.Empty;
            IMDBTitleText = string.Empty;
        }
        
        [JsonIgnore]
        public int ID { get; set; }

        [JsonProperty("id")]
        public string IMDBID { get; set; }

        [JsonProperty("ratingsSummary.aggregateRating")]
        public decimal? IMDBAggregateRatting { get; set; }

        [JsonProperty("titleType.id")]
        public string? IMDBTitleTypeID { get; set; }

        [JsonProperty("titleType.text")]
        public string? IMDBTitleTypeText { get; set; }

        [JsonProperty("titleText.text")]
        public string IMDBTitleText { get; set; }

        [JsonProperty("releaseYear.year")]
        public int? ReleaseYear { get; set; }

        [JsonProperty("primaryImage")]
        public ImageMovie? ImageMovie { get; set; }

        [JsonProperty("releaseDate")]
        public ReleaseDate? ReleaseDate { get; set; }

        [JsonProperty("plot")]
        public PlotMovie? Plot { get; set; }

        [JsonProperty("genres.genres")]
        public IList<Genre>? Genres { get; set; }

        public IList<PrincipalCastMovie>? PrincipalCastMovies { get; set; }
    }
}