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

        public string? UserId { get; set; }

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
            Context?.AddRange(movies);
        }

        public void UpdateRange(List<Movie> movies)
        {
            Context?.UpdateRange(movies);
        }

        public List<Movie> GetMovies(int page, int pageNumberMovies, string searchField)
        {
            // query = query?
            //         .Where(x => x.IMDBTitleText.Contains(searchField))
            //         .OrderByDescending(x => x.ReleaseDate!.Year)
            //         .ThenByDescending(x => x.ReleaseDate!.Month)
            //         .ThenByDescending(x => x.ReleaseDate!.Day)
            //         .Skip((page - 1) * pageNumberMovies)
            //         .Take(pageNumberMovies);

            IQueryable<Movie>? query = (
                from m in MovieQueryWithAllRelationship
                join um in Context?.UserList!
                    on new { Id = m.ID, UserId } equals new { Id = um.MovieID, UserId = um.UserId } into userMovies
                from um in userMovies.DefaultIfEmpty()
                join umr in Context?.UserMoviesRating!
                    on new { Id = m.ID, UserId } equals new { Id = umr.MovieID, UserId = umr.UserId } into userMoviesRating
                from umr in userMoviesRating.DefaultIfEmpty()
                where m.IMDBTitleText.Contains(searchField)
                select new Movie(m, um.UserId != null, umr.Rating, (from cumr in Context!.UserMoviesRating
                                                                    where cumr.MovieID == m.ID
                                                                    group cumr by cumr.MovieID into g
                                                                    select g.Average(a => a.Rating) * 2).FirstOrDefault())
                )
                .Skip((page - 1) * pageNumberMovies)
                .Take(pageNumberMovies);

            return query!.ToList();
        }

        public Movie? GetMovieById(int movieId)
        {
            Movie? movie = (
                from m in MovieQueryWithAllRelationship
                join um in Context?.UserList!
                    on new { Id = m.ID, UserId } equals new { Id = um.MovieID, UserId = um.UserId } into userMovies
                from um in userMovies.DefaultIfEmpty()
                join umr in Context?.UserMoviesRating!
                    on new { Id = m.ID, UserId } equals new { Id = umr.MovieID, UserId = umr.UserId } into userMoviesRating
                from umr in userMoviesRating.DefaultIfEmpty()
                where m.ID == movieId
                select new Movie(m, um.UserId != null, umr.Rating, (from cumr in Context!.UserMoviesRating
                                                                    where cumr.MovieID == m.ID
                                                                    group cumr by cumr.MovieID into g
                                                                    select g.Average(a => a.Rating) * 2).FirstOrDefault())
                ).FirstOrDefault();

            // IQueryable<Movie>? query = MovieQueryWithAllRelationship;

            // query = query?.Where(m => m.ID == movieId);

            return movie;
        }

        public List<Movie> GetReductedInfoMovie(int page, int pageNumberMovies, MovieTimelineRelease timelineRelease = MovieTimelineRelease.NONE, bool ignoreNoImageMovie = false)
        {
            //var query = MovieQueryWithMinimumRelationship;

            IQueryable<Movie>? query = (
                            from m in MovieQueryWithMinimumRelationship
                            join um in Context?.UserList!
                                on new { Id = m.ID, UserId } equals new { Id = um.MovieID, UserId = um.UserId } into userMovies
                            from um in userMovies.DefaultIfEmpty()
                            join umr in Context?.UserMoviesRating!
                                on new { Id = m.ID, UserId } equals new { Id = umr.MovieID, UserId = umr.UserId } into userMoviesRating
                            from umr in userMoviesRating.DefaultIfEmpty()
                            select new Movie(m, um.UserId != null, umr.Rating, (from cumr in Context!.UserMoviesRating
                                                                                where cumr.MovieID == m.ID
                                                                                group cumr by cumr.MovieID into g
                                                                                select g.Average(a => a.Rating) * 2).FirstOrDefault())
                            );

            if (query != null)
            {
                IEnumerable<Movie> enumerable = query.AsEnumerable();

                enumerable = enumerable
                        .Where(x => GetReductedInfoMovieConditional(x, timelineRelease, ignoreNoImageMovie));

                bool isDescending = !(timelineRelease == MovieTimelineRelease.PREMIERES ||
                                      timelineRelease == MovieTimelineRelease.COMING_SOON);

                return OrderByReleaseDate(enumerable, isDescending).Skip((page - 1) * pageNumberMovies).Take(pageNumberMovies).ToList();
            }

            return new List<Movie>();
        }

        public async Task<List<Movie>?> FilterNewMoviesByList(List<Movie>? movies)
        {
            IQueryable<Movie>? query = Context?.Movie;

            query = query?.Where(x => movies!.Select(y => y.IMDBID).Contains(x.IMDBID));

            return await query!.ToListAsync();
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

        private bool GetReductedInfoMovieConditional(Movie movie, MovieTimelineRelease timelineRelease, bool ignoreNoImageMovie)
        {
            if (ignoreNoImageMovie && movie.ImageMovie?.ImdbPrimaryImageUrl == null)
            {
                return false;
            }

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

        private IEnumerable<Movie> OrderByReleaseDate(IEnumerable<Movie> enumerable, bool isDescending = false)
        {
            if (isDescending)
            {
                enumerable = enumerable
                        .OrderByDescending(x => x.ReleaseDate?.Year)
                        .ThenByDescending(x => x.ReleaseDate?.Month)
                        .ThenByDescending(x => x.ReleaseDate?.Day);
            }
            else
            {
                enumerable = enumerable
                    .OrderBy(x => x.ReleaseDate?.Year)
                    .ThenBy(x => x.ReleaseDate?.Month)
                    .ThenBy(x => x.ReleaseDate?.Day);
            }

            return enumerable;
        }

        private bool PremiereDateTimeConditional(Movie movie)
        {
            DateTime releaseDate = ReleaseDateFormat(movie);

            int premiereCountSubtract = -Convert.ToInt16(DateTime.Now.DayOfWeek);

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