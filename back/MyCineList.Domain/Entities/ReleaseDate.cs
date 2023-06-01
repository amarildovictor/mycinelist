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

        private int? _Day;

        [JsonProperty("day")]
        public int? Day
        {
            get { return _Day ?? 1; }
            set { _Day = value; }
        }

        private int? _Month;

        [JsonProperty("month")]
        public int? Month
        {
            get { return _Month ?? 1; }
            set { _Month = value; }
        }

        private int? _Year;

        [JsonProperty("year")]
        public int? Year
        {
            get { return _Year ?? 1; }
            set { _Year = value; }
        }

        [JsonIgnore]
        public DateTime FormatedDate
        {
            get { return new DateTime(Year ?? 1, Month ?? 1, Day ?? 1); }
        }
        
    }
}