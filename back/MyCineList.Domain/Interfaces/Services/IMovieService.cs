using MyCineList.Domain.Entities;

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
    }
}