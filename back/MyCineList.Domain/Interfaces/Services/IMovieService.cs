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
        /// As Add method, here has adding a range of Movie.
        /// </summary>
        /// <param name="movies">Movie list.</param>
        /// <returns>Success on process or not.</returns>
        public Task AddRange(List<Movie>? movies);

        /// <summary>
        /// Similar to AddRange, the difference is with parameter that permits to pass a JSON string list response.
        /// The internal process is responsable to Deserialize the JSON string response to C# object.
        /// </summary>
        /// <param name="year">Reference year to get the movies. Based on Movie Release year.</param>
        /// <returns>Success on process or not.</returns>
        public Task<bool> AddRangeWithJSONResponseString(int year);

        /// <summary>
        /// Get the full info movie object list. With all the relationships.
        /// </summary>
        /// <param name="page">Actual page to return data.</param>
        /// <param name="searchField">The text to search.<paramref name="searchField"/>
        /// <param name="pageNumberMovies">Numbers of items to bring.</param>
        /// <returns>Full info movie list.</returns>
        List<Movie> GetMovies(int page, string searchField, int pageNumberMovies = 30);

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
        /// <param name="timelineRelease">It's the kind of release, like Premiere, Coming Soon etc.</param>
        /// <param name="pageNumberMovies">Numbers of items to bring.</param>
        /// <param name="ignoreNoImageMovie">No include the movies without image.</param>
        /// <returns>Reducted (mini-info) movie list</returns>
        List<Movie> GetReductedInfoMovie(int page, MovieTimelineRelease timelineRelease = MovieTimelineRelease.NONE, int pageNumberMovies = 30, bool ignoreNoImageMovie = false);
    }
}