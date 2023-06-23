using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCineList.Domain.Entities.UserThings;
using MyCineList.Domain.Interfaces.Repositories;
using MyCineList.Domain.Interfaces.Services;

namespace MyCineList.Domain.Services
{
    public class UserListService : IUserListService
    {
        public IUserListRepo _UserListRepo { get; }

        public UserListService(IUserListRepo userListRepo)
        {
            this._UserListRepo = userListRepo;
        }

        public List<UserList>? GetUserList(string userId)
        {
            return _UserListRepo.GetUserList(userId);
        }

        public async Task<bool> Add(UserList userList)
        {
            try
            {
                if (!_UserListRepo.HasMovieInTheList(userList.User.Id, userList.Movie.ID))
                {
                    _UserListRepo.Add(userList);

                    return await _UserListRepo.SaveChangesAsync();
                }

                return false;
            }
            catch { throw; }
        }

        public async Task Remove(string userId, int movieId)
        {
            try
            {
                _UserListRepo.Remove(userId, movieId);

                await _UserListRepo.SaveChangesAsync();
            }
            catch { throw; }
        }
    }
}