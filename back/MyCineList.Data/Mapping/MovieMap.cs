using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCineList.Domain.Entities;

namespace MyCineList.Data.Mapping
{
    public class MovieMap : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("MOVIE");

            builder.HasKey(x => x.ID);
            
            builder.HasOne(x => x.ImageMovie);
            builder.HasOne(x => x.ReleaseDate).WithOne(x => x.Movie);
            builder.HasOne(x => x.Plot).WithOne(x => x.Movie);
            
            builder.HasMany(x => x.PrincipalCastMovies);
            
            builder.Property(x => x.IMDBID).HasMaxLength(10);
            builder.Property(x => x.IMDBAggregateRatting).HasPrecision(4,2);
            builder.Property(x => x.IMDBTitleTypeID).HasMaxLength(50);
            builder.Property(x => x.IMDBTitleTypeText).HasMaxLength(50);
            builder.Property(x => x.IMDBTitleText).HasMaxLength(200);
            builder.Property(x => x.ReleaseYear);

            builder.HasIndex(x => x.IMDBID).IsUnique();
            builder.HasIndex(x => x.IMDBTitleText);
        }
    }
}