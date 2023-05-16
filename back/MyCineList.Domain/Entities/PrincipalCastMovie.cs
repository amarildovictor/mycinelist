using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCineList.Domain.Entities
{
    public class PrincipalCastMovie
    {
        public PrincipalCastMovie()
        {
            IMDBNameID = string.Empty;
            IMDBName = string.Empty;
        }

        public int ID { get; set; }

        public Movie? Movie { get; set; }

        public ImageMovie? Image { get; set; }

        public string IMDBNameID { get; set; }

        public string IMDBName { get; set; }
    }
}