using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCineList.Domain.Entities;

namespace MyCineList.Data.Mapping
{
    public class PrincipalCastMovieMap : IEntityTypeConfiguration<PrincipalCastMovie>
    {
        public void Configure(EntityTypeBuilder<PrincipalCastMovie> builder)
        {
            builder.ToTable("PRINCIPAL_CAST_MOVIE");

            builder.HasKey(x => x.ID);
            builder.HasOne(x => x.Movie);
            builder.HasOne(x => x.Image);

            builder.HasMany(x => x.PrincipalCastMovieCharacters);

            builder.Property(x => x.IMDBNameID).HasMaxLength(10);
            builder.Property(x => x.IMDBName).HasMaxLength(100);

            builder.HasIndex(x => x.IMDBName);
        }
    }
}