using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace DAL.Configurations
{
	public class WordConfig : IEntityTypeConfiguration<Word>
    {
        public void Configure(EntityTypeBuilder<Word> builder)
        {
            builder.Property(w => w.Name).IsRequired();
            builder.Property(w => w.Name).HasMaxLength(50);

            builder.HasData(
            new Word
            {
                Id=1,
                Name = "Red"
            });
        }
    }
}
