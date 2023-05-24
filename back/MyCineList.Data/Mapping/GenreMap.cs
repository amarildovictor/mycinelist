using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCineList.Domain.Entities;

namespace MyCineList.Data.Mapping
{
    public class GenreMap : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("GENRE");

            builder.HasKey(x => x.IMDBGenreID);

            builder.Property(x => x.IMDBGenreID).HasMaxLength(50);
            builder.Property(x => x.IMDBGenreText).HasMaxLength(50);
        }
    }
}