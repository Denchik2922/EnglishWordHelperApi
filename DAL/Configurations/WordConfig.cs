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

            builder.HasOne(w => w.Audio)
                   .WithOne(wa => wa.Word)
                   .HasForeignKey<WordAudio>(w => w.WordId);

            builder.HasOne(w => w.Transcription)
                   .WithOne(wt => wt.Word)
                   .HasForeignKey<WordTranscription>(w => w.WordId);

            builder.HasData(
            new Word
            {
                Id=1,
                Name = "Red"
            });
        }
    }
}
