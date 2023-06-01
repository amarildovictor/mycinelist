using MyCineList.Domain.Entities;
using MyCineList.Domain.Enumerators;

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
        /// Get the full info movie object list. With all the relationships.
        /// </summary>
        /// <param name="page">Actual page to return data.</param>
        /// <param name="pageNumberMovies">Numbers of items to bring.</param>
        /// <param name="searchField">The text to search.<paramref name="searchField"/>
        /// <returns>Full info movie list.</returns>
        List<Movie> GetMovies(int page, int pageNumberMovies, string searchField);

        /// <summary>
        /// Get the full info movie by its id.
        /// </summary>
        /// <param name="movieId">Movie id.</param>
        /// <returns></returns>
        Movie? GetMovieById(int movieId);

        /// <summary>
        /// Get the reducted info movie object list without the relationships.
        /// It has just one relation with Image relationship.
        /// </summary>
        /// <param name="page">Actual page to return data.</param>
        /// <param name="pageNumberMovies">Numbers of items to bring.</param>
        /// <param name="timelineRelease">It's the kind of release, like Premiere, Coming Soon etc.</param>
        /// <param name="ignoreNoImageMovie">No include the movies without image.</param>
        /// <returns>Reducted (mini-info) movie list</returns>
        List<Movie> GetReductedInfoMovie(int page, int pageNumberMovies, MovieTimelineRelease timelineRelease, bool ignoreNoImageMovie);

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