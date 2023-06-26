using System.ComponentModel.DataAnnotations.Schema;
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

        public Movie(Movie movie, bool? userFavorite, int? userRating, double? myCineListRating)
        {
            ID = movie.ID;
            IMDBID = movie.IMDBID;
            IMDBAggregateRatting = movie.IMDBAggregateRatting;
            IMDBTitleTypeID = movie.IMDBTitleTypeID;
            IMDBTitleTypeText = movie.IMDBTitleTypeText;
            IMDBTitleText = movie.IMDBTitleText;
            ReleaseYear = movie.ReleaseYear;
            ImageMovie = movie.ImageMovie;
            ReleaseDate = movie.ReleaseDate;
            Plot = movie.Plot;
            GenresMovie = movie.GenresMovie;
            PrincipalCastMovies = movie.PrincipalCastMovies;
            UserFavorite = userFavorite;
            UserRating = userRating;
            MyCineListRating = myCineListRating;
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
        public ICollection<GenreMovie>? GenresMovie { get; set; }

        public IList<PrincipalCastMovie>? PrincipalCastMovies { get; set; }

        [NotMapped]
        public bool? UserFavorite { get; set; }

        [NotMapped]
        public int? UserRating { get; set; }

        [NotMapped]
        private double? _MyCineListRating;

        [NotMapped]
        public double? MyCineListRating
        {
            get { return _MyCineListRating != null ? double.Round(_MyCineListRating.Value, 1) : null; }
            set { _MyCineListRating = value; }
        }
    }
}