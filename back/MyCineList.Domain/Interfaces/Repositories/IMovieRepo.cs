using MyCineList.Domain.Entities;

namespace MyCineList.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Movie repo is responsable to add the complete movie informations in its tables.
    /// </summary>
    public interface IMovieRepo
    {
        /// <summary>
        /// Add the movie in database. The EF control the relantionship adding each information in each table.
        /// </summary>
        /// <param name="movie">Movie object.</param>
        public void Add(Movie movie);

        /// <summary>
        /// Add a range of Movie. Using a Movie list.
        /// </summary>
        /// <param name="movies">Movie list.</param>
        void AddRange(List<Movie> movies);

        /// <summary>
        /// Filter from a Movie list the new ones.
        /// </summary>
        /// <param name="movies">Movie list.</param>
        /// <returns>Just the movies that aren't on database.</returns>
        Task<List<Movie>?> FilterNewMoviesByList(List<Movie> movies);

        /// <summary>
        /// Commit the actions in database.
        /// </summary>
        /// <returns>Success or not.</returns>
        public Task<bool> SaveChangesAsync();
    }
}