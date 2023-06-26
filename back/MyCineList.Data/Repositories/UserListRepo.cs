using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyCineList.Data.Context;
using MyCineList.Domain.Entities.UserThings;
using MyCineList.Domain.Interfaces.Repositories;

namespace MyCineList.Data.Repositories
{
    public class UserListRepo : IUserListRepo
    {
        private DataContext Context { get; }

        public UserListRepo(DataContext context)
        {
            this.Context = context;
        }

        public List<UserList>? GetUserList(string userId)
        {
            IQueryable<UserList>? query = Context?.UserList?.Include(x => x.Movie).Include(y => y.Movie.ImageMovie);

            query = query?
            .Where(x => x.UserId == userId)
            .OrderBy(o => o!.Movie.IMDBTitleText)
            .Select(a => new UserList
            (
                a,
                (from umr in Context!.UserMoviesRating
                 where umr.MovieID == a.MovieID && umr.UserId == a.UserId
                 select umr.Rating).FirstOrDefault(),
                (from cumr in Context!.UserMoviesRating
                 where cumr.MovieID == a.MovieID
                 group cumr by cumr.MovieID into g
                 select g.Average(a => a.Rating) * 2).FirstOrDefault()
            ));

            var queryList = query?.ToList();
            queryList?.ForEach(x => x.Movie.UserFavorite = true);

            return queryList;
        }

        public bool HasMovieInTheList(string userId, int movieId)
        {
            IQueryable<UserList>? query = Context?.UserList;

            query = query?
            .Where(x => x.User.Id == userId && x.MovieID == movieId);

            return query?.Count() > 0;
        }

        public void Add(UserList userList)
        {
            var movie = Context?.Movie?.FirstOrDefault(x => x.ID == userList.Movie.ID);
            var user = Context?.Users.FirstOrDefault(x => x.Id == userList.User.Id);

            if (movie != null && user != null)
            {
                userList.Movie = movie;
                userList.User = user;

                Context?.UserList?.Add(userList);
            }
        }

        public void Remove(string userId, int movieId)
        {
            UserList? userList = Context?.UserList?.FirstOrDefault(x => x.UserId == userId && x.MovieID == movieId);

            if (userList != null)
            {
                Context?.Remove(userList);
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