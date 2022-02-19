using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace DAL.Configurations
{
	public class WordAudioConfig : IEntityTypeConfiguration<WordAudio>
    {
        public void Configure(EntityTypeBuilder<WordAudio> builder)
        {
            builder.Property(w => w.AudioUrl).IsRequired();
        }
    }
}
