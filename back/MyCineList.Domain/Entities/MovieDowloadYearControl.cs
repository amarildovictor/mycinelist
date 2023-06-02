namespace MyCineList.Domain.Entities
{
    public class MovieDowloadYearControl
    {
        public MovieDowloadYearControl()
        {
            StartDate = DateTime.Now;
            ToUpdateNextCall = true;
        }

        public int Year { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime InfoUpdateDate { get; set; }

        public bool ToUpdateNextCall { get; set; }
    }
}