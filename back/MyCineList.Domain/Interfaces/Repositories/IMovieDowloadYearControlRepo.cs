using MyCineList.Domain.Entities;

namespace MyCineList.Domain.Interfaces.Repositories
{
    public interface IMovieDowloadYearControlRepo
    {
        /// <summary>
        /// Get next year to update the movies.
        /// </summary>
        /// <returns>Year to update.</returns>
        List<MovieDowloadYearControl>? GetNextCall();

        /// <summary>
        /// Update the year with execution date and control flag activation.
        /// </summary>
        /// <param name="movieDowloadYearControl">Control Object.</param>
        void Update(MovieDowloadYearControl movieDowloadYearControl);

        /// <summary>
        /// Commit the actions in database.
        /// </summary>
        /// <returns>Success or not.</returns>
        Task<bool> SaveChangesAsync();
    }
}