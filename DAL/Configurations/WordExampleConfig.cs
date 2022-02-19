using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace DAL.Configurations
{
	public class WordExampleConfig : IEntityTypeConfiguration<WordExample>
    {
        public void Configure(EntityTypeBuilder<WordExample> builder)
        {
            builder.Property(w => w.Example).IsRequired();
            builder.Property(w => w.Example).HasMaxLength(150);

            builder.HasData(
            new WordExample
            {
                Id = 1,
                WordId = 1,
                Example = "I have a red car",
            },
            new WordExample
            {
                Id = 2,
                WordId = 1,
                Example = "This flower is red",
            });
        }
    }
}
