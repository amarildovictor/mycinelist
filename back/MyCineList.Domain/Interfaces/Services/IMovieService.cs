using MyCineList.Domain.Entities;
using MyCineList.Domain.Enumerators;

namespace MyCineList.Domain.Interfaces.Services
{
    /// <summary>
    /// Movie service. It has a complete information about the Movie object and threat everything about it.
    /// </summary>
    public interface IMovieService
    {
        /// <summary>
        /// Add a complete information Movie. See the object Movie and fill the properties to have an eficient movie insert.
        /// </summary>
        /// <param name="movie">Movie object.</param>
        /// <returns>Success on process or not.</returns>
        public Task Add(Movie movie);

        /// <summary>
        /// As Add method, here it has adding a range of Movie.
        /// </summary>
        /// <param name="movies">Movie list.</param>
        /// <returns>Success on process or not.</returns>
        public Task AddRange(List<Movie>? movies);

        /// <summary>
        /// As Add method, here it has update a range of Movie.
        /// </summary>
        /// <param name="movies">Movie list.</param>
        /// <returns>Success on process or not.</returns>
        public Task UpdateRange(List<Movie>? movies);

        /// <summary>
        /// Get the full info movie object list. With all the relationships.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <param name="page">Actual page to return data.</param>
        /// <param name="searchField">The text to search.</param>
        /// <param name="pageNumberMovies">Numbers of items to bring.</param>
        /// <returns>Full info movie list.</returns>
        List<Movie> GetMovies(string? userId, int page, string searchField, int pageNumberMovies = 30);

        /// <summary>
        /// Get the full info movie by its id.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <param name="movieId">Movie id.</param>
        /// <returns></returns>
        Movie? GetMovieById(string? userId, int movieId);

        /// <summary>
        /// Get the reducted info movie object list without the relationships.
        /// It has just one relation with Image relationship.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <param name="page">Actual page to return data.</param>
        /// <param name="timelineRelease">It's the kind of release, like Premiere, Coming Soon etc.</param>
        /// <param name="pageNumberMovies">Numbers of items to bring.</param>
        /// <param name="ignoreNoImageMovie">No include the movies without image.</param>
        /// <returns>Reducted (mini-info) movie list</returns>
        List<Movie> GetReductedInfoMovie(string? userId, int page, MovieTimelineRelease timelineRelease = MovieTimelineRelease.NONE, int pageNumberMovies = 30, bool ignoreNoImageMovie = false);
    }
}