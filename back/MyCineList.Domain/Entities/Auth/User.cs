using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MyCineList.Domain.Entities.Auth
{
    public class User : IdentityUser
    {
        public User() : base() { }

        public User(string userName) : base(userName) { }

        public string? Password { get; set; }

        public string? Token { get; set; }
    }
}