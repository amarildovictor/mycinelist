using MyCineList.Domain.Utilities;
using Newtonsoft.Json;

namespace MyCineList.Domain.Entities
{
    [JsonConverter(typeof(JsonPathConverter))]
    public class PrincipalCastMovie
    {
        public PrincipalCastMovie()
        {
            IMDBNameID = string.Empty;
            IMDBName = string.Empty;
        }

        [JsonIgnore]
        public int ID { get; set; }
        
        [JsonIgnore]
        public Movie? Movie { get; set; }

        [JsonProperty("name.id")]
        public string IMDBNameID { get; set; }

        [JsonProperty("name.nameText.text")]
        public string IMDBName { get; set; }

        [JsonProperty("name.primaryImage")]
        public ImageMovie? Image { get; set; }

        [JsonProperty("characters")]
        public IList<PrincipalCastMovieCharacter>? PrincipalCastMovieCharacters { get; set; }
    }
}