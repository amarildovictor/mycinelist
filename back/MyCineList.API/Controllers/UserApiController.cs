using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyCineList.Domain.Entities.Auth;
using MyCineList.Domain.Entities.UserThings;
using MyCineList.Domain.Interfaces.Services;

namespace MyCineList.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserApiController : ControllerBase
    {
        private IUserListService UserListService { get; }
        private UserManager<User> UserManager { get; }

        public UserApiController(IUserListService userListService, UserManager<User> userManager)
        {
            this.UserManager = userManager;
            this.UserListService = userListService;
        }

        [HttpGet]
        [Route("movieList")]
        [Authorize]
        public IActionResult Get()
        {
            try
            {
                string userId = GetUserId();

                List<UserList>? userMoviesList = UserListService.GetUserList(userId);

                return Ok(userMoviesList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                $"Ocorreu um erro ao obter a lista de filmes: {ex.Message}. {ex}");
            }
        }

        [HttpPost]
        [Route("movieList")]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] UserList userMovieList)
        {
            try
            {
                userMovieList.User.Id = GetUserId();

                if (!await UserListService.Add(userMovieList))
                {
                    return NotFound("Filme já existente na lista.");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                $"Ocorreu um erro ao adicionar o filme na lista do usuário: {ex.Message}. {ex}");
            }
        }


        [HttpDelete("movieList/{movieId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int movieId)
        {
            try
            {
                string userId = GetUserId();

                await UserListService.Remove(userId, movieId);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                $"Ocorreu um erro ao remover o filme {movieId} na lista do usuário: {ex.Message}. {ex}");
            }
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }
    }
}