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
        /// <returns>Full info movie list.</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Movie>? movies = MovieService.GetMovies();

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
        /// <returns>Reducted (mini-info) movie list</returns>
        [HttpGet("titles/{movieTimelineRelease}")]
        public IActionResult GetReductedInfoMovie(MovieTimelineRelease movieTimelineRelease)
        {
            try
            {
                List<Movie>? movies = MovieService.GetReductedInfoMovie(movieTimelineRelease);

                return Ok(movies);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, $"Erro ao obter a lista reduzida de filmes. Erro: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Responsable to add a movie list based on a release date. It is used the public MovieDataBase API.
        /// </summary>
        /// <param name="year">Year of Movies release date.</param>
        /// <returns>HTTP Action Result.</returns>
        [HttpPost]
        public async Task<IActionResult> Post(int year)
        {
            try
            {
                if (await MovieService.AddRangeWithJSONResponseString(year))
                    return Ok();

                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, $"Erro ao tentar executar a inclusão de filmes. Erro: {ex.Message}");
                throw;
            }
        }
    }
}