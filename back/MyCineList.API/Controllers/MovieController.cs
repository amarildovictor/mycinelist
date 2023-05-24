using Microsoft.AspNetCore.Mvc;
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