using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCineList.Domain.Entities.UserThings;

namespace MyCineList.Data.Mapping.UserThings
{
    public class UserListMap : IEntityTypeConfiguration<UserList>
    {
        public void Configure(EntityTypeBuilder<UserList> builder)
        {
            builder.ToTable("USER_MOVIE_LIST");

            builder.HasKey(x => x.ID);
            builder.Navigation(x => x.Movie);
            builder.Navigation(x => x.User);

            builder.Property(x => x.Date);
            builder.Property(x => x.Rating);
            builder.Property(x => x.IsToEmailNotificate);

            builder.HasIndex(x => new { x.UserId, x.MovieID}).IsUnique();
        }
    }
}