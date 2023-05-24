using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyCineList.Domain.Entities
{
    public class GenreMovie
    {
        public GenreMovie()
        {
            GenresIMDBGenreID = string.Empty;
        }

        [JsonProperty("id")]
        public string GenresIMDBGenreID { get; set; }

        [JsonIgnore]
        public int MovieID { get; set; }
        
        [JsonIgnore]
        public Genre? Genre { get; set; }

        [JsonIgnore]
        public Movie? Movie { get; set; }
    }
}