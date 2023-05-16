using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCineList.Domain.Entities
{
    public class PlotMovie
    {
        public PlotMovie()
        {
            IMDBPlainText = string.Empty;
        }

        public int ID { get; set; }

        public Movie? Movie { get; set; }

        public string IMDBPlainText { get; set; }

        public string? IMDBLanguageID { get; set; }
    }
}