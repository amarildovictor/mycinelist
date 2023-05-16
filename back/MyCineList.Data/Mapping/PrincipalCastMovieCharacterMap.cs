using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCineList.Domain.Entities;

namespace MyCineList.Data.Mapping
{
    public class PrincipalCastMovieCharacterMap : IEntityTypeConfiguration<PrincipalCastMovieCharacter>
    {
        public void Configure(EntityTypeBuilder<PrincipalCastMovieCharacter> builder)
        {
            builder.ToTable("PRINCIPAL_CAST_MOVIE_CHARACTER");

            builder.HasKey(x => x.ID);
            builder.HasOne(x => x.PrincipalCastMovie);

            builder.Property(x => x.IMDBCharacterName).HasMaxLength(100);

            builder.HasIndex(x => x.IMDBCharacterName);
        }
    }
}