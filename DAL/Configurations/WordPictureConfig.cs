using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace DAL.Configurations
{
	public class WordPictureConfig : IEntityTypeConfiguration<WordPicture>
    {
        public void Configure(EntityTypeBuilder<WordPicture> builder)
        {
            builder.Property(w => w.PictureUrl).IsRequired();

            builder.HasOne(we => we.Word)
                   .WithMany(w => w.Pictures)
                   .HasForeignKey(w => w.WordId);
        }
    }
}
