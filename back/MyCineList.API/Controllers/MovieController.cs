using Microsoft.AspNetCore.Mvc;
using MyCineList.Domain.Entities;
using MyCineList.Domain.Enumerators;
using MyCineList.Domain.Interfaces.Services;

namespace MyCineList.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        public IMovieService MovieService { get; }

        public MovieController(IMovieService movieService)
        {
            this.MovieService = movieService;
        }

        /// <summary>
        /// Get the full info movie object list. With all the relationships.
        /// </summary>
        /// <param name="searchText">Search text field.</param>
        /// <param name="pageNumberMovies">Numbers of items to bring.</param>
        /// <param name="page">Actual page to return data.</param>
        /// <returns>Full info movie list.</returns>
        [HttpGet("search")]
        public IActionResult Get(string? searchText = null, int pageNumberMovies = 30, int page = 1)
        {
            try
            {
                List<Movie>? movies = MovieService.GetMovies(page, searchText ?? string.Empty, pageNumberMovies);

                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, $"Erro ao obter a lista de filmes. Erro: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// /// Get the full info movie by its id.
        /// </summary>
        /// <param name="movieId">Movie id.</param>
        /// <returns></returns>
        [HttpGet("{movieId}")]
        public IActionResult GetMovieById(int movieId)
        {
            try
            {
                Movie? movie = MovieService.GetMovieById(movieId);

                return Ok(movie);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, $"Erro ao obter a lista de filmes. Erro: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Get the reducted info movie object list without the relationships.
        /// It has just one relation with Image relationship.
        /// </summary>
        /// <param name="movieTimelineRelease">It's the kind of release, like Premiere, Coming Soon etc.</param>
        /// <param name="pageNumberMovies">Numbers of top items to bring.</param>
        /// <param name="ignoreNoImageMovie">No include the movies without image.</param>
        /// <param name="page">Actual page to return data.</param>
        /// <returns>Reducted (mini-info) movie list</returns>
        [HttpGet("search/timeline/{movieTimelineRelease}")]
        public IActionResult GetReductedInfoMovie(MovieTimelineRelease movieTimelineRelease, int pageNumberMovies = 30, bool ignoreNoImageMovie = false, int page = 1)
        {
            try
            {
                List<Movie>? movies = MovieService.GetReductedInfoMovie(page, movieTimelineRelease, pageNumberMovies, ignoreNoImageMovie);

                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, $"Erro ao obter a lista reduzida de filmes. Erro: {ex.Message}");
                throw;
            }
        }
    }
}