using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCineList.Domain.Entities;
using MyCineList.Domain.Interfaces.Repositories;
using MyCineList.Data.Context;
using Microsoft.EntityFrameworkCore;
using MyCineList.Domain.Enumerators;

namespace MyCineList.Data.Repositories
{
    public class MovieRepo : IMovieRepo
    {
        public DataContext? Context { get; }

        #region "PUBLIC METHODS"
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

        public List<Movie> GetMovies(int pageNumberMovies)
        {
            IQueryable<Movie>? query = MovieQueryWithAllRelationship;
            
            query = query?
                    .OrderByDescending(x => x.ReleaseDate!.Year)
                    .ThenByDescending(x => x.ReleaseDate!.Month)
                    .ThenByDescending(x => x.ReleaseDate!.Day)
                    .Take(pageNumberMovies);
            
            return query!.ToList();
        }

        public Movie? GetMovieById(int movieId)
        {
            IQueryable<Movie>? query = MovieQueryWithAllRelationship;
            
            query = query?.Where(m => m.ID == movieId);
            
            return query!.FirstOrDefault();
        }

        public List<Movie> GetReductedInfoMovie(int pageNumberMovies, MovieTimelineRelease timelineRelease = MovieTimelineRelease.NONE)
        {
            IQueryable<Movie>? query = MovieQueryWithMinimumRelationship;

            query = query?
                    .AsEnumerable()
                    .Where(x => TimeLineReleaseConditional(x, timelineRelease)).AsQueryable();

            bool isDescending = !(timelineRelease == MovieTimelineRelease.PREMIERES ||
                                  timelineRelease == MovieTimelineRelease.COMING_SOON);

            return OrderByReleaseDate(query, isDescending)!.Take(pageNumberMovies).ToList();
        }

        public async Task<List<Movie>?> FilterNewMoviesByList(List<Movie> movies)
        {
            IQueryable<Movie>? query = Context?.Movie;

            query = query?.Where(x => movies.Select(y => y.IMDBID).Contains(x.IMDBID));

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
            catch { throw; }
        }

        #endregion

        #region "PRIVATE METHODS"
        
        private bool TimeLineReleaseConditional(Movie movie, MovieTimelineRelease timelineRelease)
        {
            switch (timelineRelease)
            {
                case MovieTimelineRelease.PREMIERES:
                    return PremiereDateTimeConditional(movie);
                case MovieTimelineRelease.COMING_SOON:
                    return ComingSoonDateTimeConditional(movie);
                case MovieTimelineRelease.ON_DISPLAY:
                    return OnDisplayDateTimeConditional(movie);
                case MovieTimelineRelease.IN_FAREWELL:
                    return InFarewellDateTimeConditional(movie);
                default:
                    return true;
            }
        }

        private IQueryable<Movie>? OrderByReleaseDate(IQueryable<Movie>? query, bool isDescending = false)
        {
            if (isDescending)
            {
                query = query?
                        .OrderByDescending(x => x.ReleaseDate!.Year)
                        .ThenByDescending(x => x.ReleaseDate!.Month)
                        .ThenByDescending(x => x.ReleaseDate!.Day);
            }
            else
            {
                query = query?
                    .OrderBy(x => x.ReleaseDate!.Year)
                    .ThenBy(x => x.ReleaseDate!.Month)
                    .ThenBy(x => x.ReleaseDate!.Day);    
            }

            return query;
        }

        private bool PremiereDateTimeConditional(Movie movie)
        {
            DateTime releaseDate = ReleaseDateFormat(movie);

            int premiereCountSubtract = - Convert.ToInt16(DateTime.Now.DayOfWeek);

            return DateTime.Now.AddDays(GetPremiereCountAdd()) >= releaseDate &&
                   DateTime.Now.AddDays(premiereCountSubtract) <= releaseDate;
        }

        private bool ComingSoonDateTimeConditional(Movie movie)
        {
            const int HOW_MANY_DAYS_TO_CONSIDER_COMING_SOON = 60;
            DateTime releaseDate = ReleaseDateFormat(movie);

            return releaseDate > DateTime.Now.AddDays(GetPremiereCountAdd()) &&
                   releaseDate < DateTime.Now.AddDays(GetPremiereCountAdd() + HOW_MANY_DAYS_TO_CONSIDER_COMING_SOON);
        }

        private bool OnDisplayDateTimeConditional(Movie movie)
        {
            const int HOW_MANY_DAYS_TO_CONSIDER_ON_DISPLAY = -60;
            DateTime releaseDate = ReleaseDateFormat(movie);

            return releaseDate >= DateTime.Now.AddDays(HOW_MANY_DAYS_TO_CONSIDER_ON_DISPLAY) &&
                   releaseDate <= DateTime.Now;
        }

        private bool InFarewellDateTimeConditional(Movie movie)
        {
            const int HOW_MANY_DAYS_TO_CONSIDER_IN_FAREWELL = -30;
            DateTime releaseDate = ReleaseDateFormat(movie);

            return releaseDate >= DateTime.Now.AddDays(2 * HOW_MANY_DAYS_TO_CONSIDER_IN_FAREWELL) &&
                   releaseDate <= DateTime.Now.AddDays(HOW_MANY_DAYS_TO_CONSIDER_IN_FAREWELL);
        }

        private DateTime ReleaseDateFormat(Movie movie)
        {
            if (movie.ReleaseDate?.Year == null)
            {
                return DateTime.MinValue;
            }

            return new DateTime(movie.ReleaseDate!.Year ?? 1, movie.ReleaseDate!.Month ?? 1, movie.ReleaseDate!.Day ?? 1);
        }
        
        private int GetPremiereCountAdd()
        {
            const int DAYS_OF_WEEK_NUMBER = 7;

            return DAYS_OF_WEEK_NUMBER - Convert.ToInt16(DateTime.Now.DayOfWeek);
        }

        private IQueryable<Movie>? MovieQueryWithAllRelationship
        {
            get 
            {
                return Context?.Movie?.Include(i => i.ImageMovie).Include(i => i.ReleaseDate).Include(i => i.Plot).Include(i => i.GenresMovie);
            }
        }

        private IQueryable<Movie>? MovieQueryWithMinimumRelationship
        {
            get 
            {
                return Context?.Movie?.Include(i => i.ImageMovie).Include(i => i.ReleaseDate);
            }
        }

        #endregion
    }
}