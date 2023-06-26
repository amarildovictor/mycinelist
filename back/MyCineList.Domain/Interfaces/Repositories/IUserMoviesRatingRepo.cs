using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCineList.Domain.Entities.UserThings;

namespace MyCineList.Domain.Interfaces.Repositories
{
    public interface IUserMoviesRatingRepo
    {
        bool HasMovieInTheList(string userId, int movieId);

        void Add(UserMoviesRating userList);

        void Update(UserMoviesRating userMoviesRating);

        void Remove(string userId, int movieId);

        Task<bool> SaveChangesAsync();
    }
}