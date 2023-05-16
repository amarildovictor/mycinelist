using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCineList.Domain.Entities
{
    public class PrincipalCastMovieCharacter
    {
        public PrincipalCastMovieCharacter()
        {
            IMDBCharacterName = string.Empty;
        }

        public int ID { get; set; }

        public PrincipalCastMovie? PrincipalCastMovie { get; set; }

        public string IMDBCharacterName { get; set; }
    }
}