using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCineList.Domain.Entities
{
    public class Movie
    {
        public Movie()
        {
            IMDBID = string.Empty;
            IMDBTiltleText = string.Empty;
        }
        
        public int ID { get; set; }

        public ImageMovie? ImageMovie { get; set; }

        public string IMDBID { get; set; }

        public decimal? IMDBAggregateRatting { get; set; }

        public string? IMDBTitleTypeID { get; set; }

        public string IMDBTiltleText { get; set; }

        public int? ReleaseYear { get; set; }

        public DateTime? ReleaseDate { get; set; }
    }
}