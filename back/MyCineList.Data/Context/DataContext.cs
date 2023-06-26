using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyCineList.Data.Mapping;
using MyCineList.Data.Mapping.UserThings;
using MyCineList.Domain.Entities;
using MyCineList.Domain.Entities.Auth;
using MyCineList.Domain.Entities.UserThings;

namespace MyCineList.Data.Context
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<ImageMovie>? ImageMovie { get; set; }

        public DbSet<Movie>? Movie { get; set; }

        public DbSet<PlotMovie>? PlotMovie { get; set; }

        public DbSet<GenreMovie>? GenreMovie { get; set; }

        public DbSet<Genre>? Genres { get; set; }

        public DbSet<ReleaseDate>? ReleaseDates { get; set; }

        public DbSet<PrincipalCastMovie>? PrincipalCastMovie { get; set; }

        public DbSet<PrincipalCastMovieCharacter>? PrincipalCastMovieCharacter { get; set; }

        public DbSet<MovieDowloadYearControl>? MovieDowloadYearControl { get; set; }

        public DbSet<UserList>? UserList { get; set; }

        public DbSet<UserMoviesRating>? UserMoviesRating { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ImageMovieMap());
            modelBuilder.ApplyConfiguration(new MovieMap());
            modelBuilder.ApplyConfiguration(new GenreMovieMap());
            modelBuilder.ApplyConfiguration(new GenreMap());
            modelBuilder.ApplyConfiguration(new PlotMovieMap());
            modelBuilder.ApplyConfiguration(new PrincipalCastMovieMap());
            modelBuilder.ApplyConfiguration(new PrincipalCastMovieCharacterMap());
            modelBuilder.ApplyConfiguration(new ReleaseDateMap());
            modelBuilder.ApplyConfiguration(new MovieDowloadYearControlMap());
            modelBuilder.ApplyConfiguration(new UserListMap());
            modelBuilder.ApplyConfiguration(new UserMoviesRatingMap());
        }
    }
}