using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MyCineList.Domain.Entities
{
    public class Genre
    {
        public Genre()
        {
            IMDBGenreID = string.Empty;
            IMDBGenreText = string.Empty;
        }

        [JsonProperty("id")]
        public string IMDBGenreID { get; set; }

        [JsonProperty("text")]
        public string IMDBGenreText { get; set; }

        [JsonIgnore]
        public ICollection<GenreMovie>? GenreMovie { get; set; }
    }
}