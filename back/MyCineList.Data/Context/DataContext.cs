using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyCineList.Data.Mapping;
using MyCineList.Domain.Entities;

namespace MyCineList.Data.Context
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<ImageMovie>? ImageMovie { get; set; }

        public DbSet<Movie>? Movie { get; set; }

        public DbSet<PlotMovie>? PlotMovie { get; set; }

        public DbSet<PrincipalCastMovie>? PrincipalCastMovie { get; set; }

        public DbSet<PrincipalCastMovieCharacter>? principalCastMovieCharacter { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ImageMovieMap());
            modelBuilder.ApplyConfiguration(new MovieMap());
            modelBuilder.ApplyConfiguration(new GenreMap());
            modelBuilder.ApplyConfiguration(new PlotMovieMap());
            modelBuilder.ApplyConfiguration(new PrincipalCastMovieMap());
            modelBuilder.ApplyConfiguration(new PrincipalCastMovieCharacterMap());
            modelBuilder.ApplyConfiguration(new ReleaseDateMap());
        }
    }
}