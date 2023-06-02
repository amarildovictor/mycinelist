using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCineList.Data.Context;
using MyCineList.Domain.Entities;
using MyCineList.Domain.Interfaces.Repositories;

namespace MyCineList.Data.Repositories
{
    public class MovieDowloadYearControlRepo : IMovieDowloadYearControlRepo
    {
        public DataContext? Context { get; }

        public MovieDowloadYearControlRepo(DataContext context)
        {
            Context = context;
        }
        
        public List<MovieDowloadYearControl>? GetNextCall()
        {
            IQueryable<MovieDowloadYearControl>? query = Context?.MovieDowloadYearControl;

            query = query?
                    .Where(x => x.ToUpdateNextCall);

            return query?.ToList();
        }

        public void Update(MovieDowloadYearControl movieDowloadYearControl)
        {
            Context?.Update(movieDowloadYearControl);
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