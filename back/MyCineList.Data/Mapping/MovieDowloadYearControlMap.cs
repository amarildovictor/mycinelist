using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCineList.Domain.Entities;

namespace MyCineList.Data.Mapping
{
    public class MovieDowloadYearControlMap : IEntityTypeConfiguration<MovieDowloadYearControl>
    {
        public void Configure(EntityTypeBuilder<MovieDowloadYearControl> builder)
        {
            builder.ToTable("MOVIE_DOWNLOAD_YEAR_CONTROL");

            builder.HasKey(x => x.Year);

            builder.Property(x => x.Year).ValueGeneratedNever();
            builder.Property(x => x.StartDate);
            builder.Property(x => x.InfoUpdateDate);
            builder.Property(x => x.ToUpdateNextCall);
        }
    }
}