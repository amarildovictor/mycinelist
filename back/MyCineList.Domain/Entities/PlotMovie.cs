using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCineList.Domain.Utilities;
using Newtonsoft.Json;

namespace MyCineList.Domain.Entities
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class PlotMovie
    {
        public PlotMovie()
        {
            IMDBPlainText = string.Empty;
        }

        [JsonIgnore]
        public int ID { get; set; }

        [JsonIgnore]
        public Movie? Movie { get; set; }

        [JsonIgnore]
        public int MovieID { get; set; }
        
        [JsonProperty("plotText.plainText")]
        public string IMDBPlainText { get; set; }

        [JsonProperty("language.id")]
        public string? IMDBLanguageID { get; set; }
    }
}