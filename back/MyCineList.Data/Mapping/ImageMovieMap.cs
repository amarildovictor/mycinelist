using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCineList.Domain.Entities;

namespace MyCineList.Data.Mapping
{
    public class ImageMovieMap : IEntityTypeConfiguration<ImageMovie>
    {
        public void Configure(EntityTypeBuilder<ImageMovie> builder)
        {
            builder.ToTable("IMAGE_MOVIE");
            
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ImdbPrimaryImageUrl).HasMaxLength(1000);
            builder.Property(x => x.SmallImageUrl).HasMaxLength(1000);
            builder.Property(x => x.MediumImageUrl).HasMaxLength(1000);
            builder.Property(x => x.Width);
            builder.Property(x => x.Height);

            builder.HasIndex(x => x.ImdbPrimaryImageUrl);
        }
    }
}