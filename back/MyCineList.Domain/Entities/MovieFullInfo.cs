namespace MyCineList.Domain.Entities
{
    public class MovieFullInfo
    {
        public MovieFullInfo()
        {
            Movie = new Movie();
            PrincipalCastMovie = new PrincipalCastMovie();
        }

        public Movie Movie { get; set; }

        public PrincipalCastMovie PrincipalCastMovie { get; set; }
    }
}