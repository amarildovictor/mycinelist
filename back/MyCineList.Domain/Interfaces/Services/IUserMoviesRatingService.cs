using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCineList.Domain.Entities.UserThings;

namespace MyCineList.Domain.Interfaces.Services
{
    public interface IUserMoviesRatingService
    {
        Task<bool> Add(UserMoviesRating userMoviesRating);

        Task Remove(string userId, int movieId);
    }
}