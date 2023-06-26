using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCineList.Data.Context;
using MyCineList.Domain.Entities.UserThings;
using MyCineList.Domain.Interfaces.Repositories;

namespace MyCineList.Data.Repositories
{
    public class UserMoviesRatingRepo : IUserMoviesRatingRepo
    {
        private DataContext Context { get; }

        public UserMoviesRatingRepo(DataContext context)
        {
            this.Context = context;
        }

        public bool HasMovieInTheList(string userId, int movieId)
        {
            IQueryable<UserMoviesRating>? query = Context?.UserMoviesRating;

            query = query?
            .Where(x => x.User.Id == userId && x.MovieID == movieId);

            return query?.Count() > 0;
        }

        public void Add(UserMoviesRating userMoviesRating)
        {
            var movie = Context?.Movie?.FirstOrDefault(x => x.ID == userMoviesRating.Movie.ID);
            var user = Context?.Users.FirstOrDefault(x => x.Id == userMoviesRating.User.Id);

            if (movie != null && user != null)
            {
                userMoviesRating.Movie = movie;
                userMoviesRating.User = user;

                Context?.UserMoviesRating?.Add(userMoviesRating);
            }
        }

        public void Update(UserMoviesRating userMoviesRating)
        {
            UserMoviesRating? userMoviesRatingToUpdate = Context?.UserMoviesRating?
                                    .Where(x => x.User.Id == userMoviesRating.User.Id && x.MovieID == userMoviesRating.Movie.ID)?.FirstOrDefault();

            if (userMoviesRatingToUpdate != null)
            {
                userMoviesRatingToUpdate.Date = DateTime.Now;
                userMoviesRatingToUpdate.Rating = userMoviesRating.Rating;

                Context?.UserMoviesRating?.Update(userMoviesRatingToUpdate);
            }
        }

        public void Remove(string userId, int movieId)
        {
            UserMoviesRating? userMoviesRating = Context?.UserMoviesRating?.FirstOrDefault(x => x.UserId == userId && x.MovieID == movieId);

            if (userMoviesRating != null)
            {
                Context?.UserMoviesRating?.Remove(userMoviesRating);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return (Context != null && await Context.SaveChangesAsync() > 0);
            }
            catch { throw; }
        }
    }
}