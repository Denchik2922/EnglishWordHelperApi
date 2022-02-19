using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace DAL.Configurations
{
	public class WordTranscriptionConfig : IEntityTypeConfiguration<WordTranscription>
    {
        public void Configure(EntityTypeBuilder<WordTranscription> builder)
        {
            builder.Property(w => w.Name).IsRequired();
            builder.Property(w => w.Name).HasMaxLength(150);

            builder.HasData(
            new WordTranscription
            {
                Id = 1,
                WordId = 1,
                Name = "red",
            });
        }
    }
}
