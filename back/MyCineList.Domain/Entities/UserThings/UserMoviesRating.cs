using MyCineList.Domain.Entities.Auth;

namespace MyCineList.Domain.Entities.UserThings
{
    public class UserMoviesRating
    {
        public UserMoviesRating()
        {
            Movie = new Movie();
            User = new User();
            Date = DateTime.Now;
        }
        public int ID { get; set; }

        public User User { get; set; }

        public string? UserId { get; set; }

        public Movie Movie { get; set; }

        public int MovieID { get; set; }

        public DateTime Date { get; set; }

        public int? Rating { get; set; }
    }
}