using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCineList.Domain.Entities.UserThings;

namespace MyCineList.Data.Mapping.UserThings
{
    public class UserMoviesRatingMap : IEntityTypeConfiguration<UserMoviesRating>
    {
        public void Configure(EntityTypeBuilder<UserMoviesRating> builder)
        {
            builder.ToTable("USER_MOVIES_RATING");

            builder.HasKey(x => x.ID);
            builder.Navigation(x => x.Movie);
            builder.Navigation(x => x.User);

            builder.Property(x => x.Date);
            builder.Property(x => x.Rating);

            builder.HasIndex(x => new { x.UserId, x.MovieID}).IsUnique();
        }
    }
}