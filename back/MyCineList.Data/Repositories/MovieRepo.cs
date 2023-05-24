using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCineList.Domain.Entities;
using MyCineList.Domain.Interfaces.Repositories;
using MyCineList.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace MyCineList.Data.Repositories
{
    public class MovieRepo : IMovieRepo
    {
        public DataContext? Context { get; }

        public MovieRepo(DataContext context)
        {
            Context = context;
        }

        public void Add(Movie movie)
        {
            Context?.Add(movie);
        }

        public void AddRange(List<Movie> movies)
        {
            Context?.AddRangeAsync(movies);
        }

        public async Task<List<Movie>?> FilterNewMoviesByList(List<Movie> movies)
        {
            IQueryable<Movie>? query = Context?.Movie;

            query = query?.AsNoTracking()
                          .Where(x => movies.Select(y => y.IMDBID).Contains(x.IMDBID));

            var newMoviesList = await query!.ToListAsync();
            newMoviesList.ForEach(x => movies.RemoveAll(y => y.IMDBID == x.IMDBID));

            return movies;
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return (Context != null && await Context.SaveChangesAsync() > 0);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}