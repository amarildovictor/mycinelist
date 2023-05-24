using Newtonsoft.Json;

namespace MyCineList.Domain.Entities
{
    public class ReleaseDate
    {
        [JsonIgnore]
        public int ID { get; set; }

        [JsonIgnore]
        public Movie? Movie { get; set; }
        
        [JsonIgnore]
        public int MovieID { get; set; }

        [JsonProperty("day")]
        public int? Day { get; set; }

        [JsonProperty("month")]
        public int? Month { get; set; }

        [JsonProperty("year")]
        public int? Year { get; set; }
    }
}