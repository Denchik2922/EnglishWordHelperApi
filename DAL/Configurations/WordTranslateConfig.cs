using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace DAL.Configurations
{
	public class WordTranslateConfig : IEntityTypeConfiguration<WordTranslate>
    {
        public void Configure(EntityTypeBuilder<WordTranslate> builder)
        {
            builder.Property(w => w.Name).IsRequired();
            builder.Property(w => w.Name).HasMaxLength(150);

            builder.HasData(
            new WordTranslate
            {
                Id = 1,
                WordId = 1,
                Name = "Красный",
            });
        }
    }
}
