using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCineList.Domain.Entities.Auth
{
    public class JWTSettings
    {
        public string? SecretKey { get; set; }
    }
}