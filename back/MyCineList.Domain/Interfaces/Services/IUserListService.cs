using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCineList.Domain.Entities.UserThings;

namespace MyCineList.Domain.Interfaces.Services
{
    public interface IUserListService
    {
        List<UserList>? GetUserList(string userId);

        Task<bool> Add(UserList userList);

        Task Remove(string userId, int movieId);
    }
}