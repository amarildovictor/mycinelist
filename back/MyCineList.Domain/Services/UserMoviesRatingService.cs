using MyCineList.Domain.Entities.UserThings;
using MyCineList.Domain.Interfaces.Repositories;
using MyCineList.Domain.Interfaces.Services;

namespace MyCineList.Domain.Services
{
    public class UserMoviesRatingService : IUserMoviesRatingService
    {
        public IUserMoviesRatingRepo _UserMoviesRatingRepo { get; }

        public UserMoviesRatingService(IUserMoviesRatingRepo userMoviesRatingRepo)
        {
            this._UserMoviesRatingRepo = userMoviesRatingRepo;
        }

        public async Task<bool> Add(UserMoviesRating userMoviesRating)
        {
            try
            {
                if (!_UserMoviesRatingRepo.HasMovieInTheList(userMoviesRating.User.Id, userMoviesRating.Movie.ID))
                {
                    _UserMoviesRatingRepo.Add(userMoviesRating);
                }
                else
                {
                    _UserMoviesRatingRepo.Update(userMoviesRating);
                }

                return await _UserMoviesRatingRepo.SaveChangesAsync();
            }
            catch { throw; }
        }

        public async Task Remove(string userId, int movieId)
        {
            try
            {
                _UserMoviesRatingRepo.Remove(userId, movieId);

                await _UserMoviesRatingRepo.SaveChangesAsync();
            }
            catch { throw; }
        }
    }
}