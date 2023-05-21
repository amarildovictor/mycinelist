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

        [JsonIgnore]
        public int ID { get; set; }

        [JsonProperty("id")]
        public string IMDBGenreID { get; set; }

        [JsonProperty("text")]
        public string IMDBGenreText { get; set; }
    }
}