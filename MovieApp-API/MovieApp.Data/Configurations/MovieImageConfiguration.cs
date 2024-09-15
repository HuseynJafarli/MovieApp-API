using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MovieApp.Core.Entities;

namespace MovieApp.Data.Configurations
{
    public class MovieImageConfiguration : IEntityTypeConfiguration<MovieImage>
    {
        public void Configure(EntityTypeBuilder<MovieImage> builder)
        {
            builder.Property(x => x.ImageUrl).IsRequired().HasMaxLength(100);
        }
    }
}
