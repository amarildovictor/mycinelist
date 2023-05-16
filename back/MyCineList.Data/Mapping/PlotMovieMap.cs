using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCineList.Domain.Entities;

namespace MyCineList.Data.Mapping
{
    public class PlotMovieMap : IEntityTypeConfiguration<PlotMovie>
    {
        public void Configure(EntityTypeBuilder<PlotMovie> builder)
        {
            builder.ToTable("PLOT_MOVIE");

            builder.HasKey(x => x.ID);
            builder.HasOne(x => x.Movie);

            builder.Property(x => x.IMDBPlainText);
            builder.Property(x => x.IMDBLanguageID).HasMaxLength(5);

            builder.HasIndex(x => x.IMDBLanguageID);
        }
    }
}