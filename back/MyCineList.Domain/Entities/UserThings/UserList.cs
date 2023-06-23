using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MyCineList.Domain.Entities.Auth;

namespace MyCineList.Domain.Entities.UserThings
{
    public class UserList
    {
        public UserList()
        {
            User = new User();
            Movie = new Movie();
            Date = DateTime.Now;
            IsToEmailNotificate = true;
        }

        public int ID { get; set; }

        public User User { get; set; }

        public string? UserId { get; set; }

        public Movie Movie { get; set; }

        public int MovieID { get; set; }

        public DateTime Date { get; set; }

        public int? Rating { get; set; }

        public bool? IsToEmailNotificate { get; set; }
    }
}