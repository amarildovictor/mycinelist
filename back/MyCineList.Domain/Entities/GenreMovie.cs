using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCineList.Domain.Entities
{
    public class GenreMovie
    {
        public GenreMovie()
        {
            IMDBGenreID = string.Empty;
        }

        public int ID { get; set; }

        public Movie? Movie { get; set; }

        public string IMDBGenreID { get; set; }
    }
}