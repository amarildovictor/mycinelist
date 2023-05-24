using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCineList.Domain.Entities
{
    public class DefaultJSONResponse<T>
    {
        public int page { get; set; }

        public string? next { get; set; }

        public int entries { get; set; }

        public List<T>? results { get; set; }
    }
}