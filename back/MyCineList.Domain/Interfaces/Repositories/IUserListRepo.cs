using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCineList.Domain.Entities.UserThings;

namespace MyCineList.Domain.Interfaces.Repositories
{
    public interface IUserListRepo
    {
        List<UserList>? GetUserList(string userId);

        bool HasMovieInTheList(string userId, int movieId);

        void Add(UserList userList);

        void Remove(string userId, int movieId);

        Task<bool> SaveChangesAsync();
    }
}