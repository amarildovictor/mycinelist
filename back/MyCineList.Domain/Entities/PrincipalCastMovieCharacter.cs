using Newtonsoft.Json;

namespace MyCineList.Domain.Entities
{
    public class PrincipalCastMovieCharacter
    {
        public PrincipalCastMovieCharacter()
        {
            IMDBCharacterName = string.Empty;
        }

        [JsonIgnore]
        public int ID { get; set; }

        [JsonIgnore]
        public PrincipalCastMovie? PrincipalCastMovie { get; set; }

        [JsonProperty("name")]
        public string IMDBCharacterName { get; set; }
    }
}