using System;
using System.Collections.Generic;
using System.Linq;
using MyCineList.Domain.Entities;
using MyCineList.Domain.Enumerators;
using MyCineList.Domain.Interfaces.Repositories;
using MyCineList.Domain.Interfaces.Services;

namespace MyCineList.Domain.Services
{
    public class MovieService : IMovieService
    {
        public IMovieRepo MovieRepo { get; }

        public MovieService(IMovieRepo movieRepo)
        {
            this.MovieRepo = movieRepo;
        }

        public async Task Add(Movie movie)
        {
            try
            {
                MovieRepo.Add(movie);

                await MovieRepo.SaveChangesAsync();
            }
            catch { throw; }
        }

        public async Task AddRange(List<Movie>? movies)
        {
            try
            {
                if (movies != null && movies.Count > 0)
                {
                    MovieRepo.AddRange(movies);

                    await MovieRepo.SaveChangesAsync();
                }
            }
            catch { throw; }
        }

        public async Task UpdateRange(List<Movie>? movies)
        {
            try
            {
                if (movies != null && movies.Count > 0)
                {
                    MovieRepo.UpdateRange(movies);

                    await MovieRepo.SaveChangesAsync();
                }
            }
            catch { throw; }
        }

        public List<Movie> GetMovies(string? userId, int page, string searchField, int pageNumberMovies = 30)
        {
            try
            {
                MovieRepo.UserId = userId;
                return MovieRepo.GetMovies(page, pageNumberMovies, searchField);
            }
            catch { throw; }
        }

        public Movie? GetMovieById(string? userId, int movieId)
        {
            try
            {
                MovieRepo.UserId = userId;
                return MovieRepo.GetMovieById(movieId);
            }
            catch { throw; }
        }

        public List<Movie> GetReductedInfoMovie(string? userId, int page, MovieTimelineRelease timelineRelease = MovieTimelineRelease.NONE, int pageNumberMovies = 30, bool ignoreNoImageMovie = false)
        {
            try
            {
                MovieRepo.UserId = userId;
                return MovieRepo.GetReductedInfoMovie(page, pageNumberMovies, timelineRelease, ignoreNoImageMovie);
            }
            catch { throw; }
        }
    }
}