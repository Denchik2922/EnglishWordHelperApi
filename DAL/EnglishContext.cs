using DAL.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DAL
{
	public class EnglishContext : IdentityDbContext<AppUser>
	{
		public EnglishContext(DbContextOptions<EnglishContext> options) : base(options) { }

		public DbSet<Word> Words { get; set; }
		public DbSet<WordAudio> WordAudios { get; set; }
		public DbSet<WordExample> WordExamples { get; set; }
		public DbSet<WordPicture> WordPictures { get; set; }
		public DbSet<WordTranscription> WordTranscriptions { get; set; }
		public DbSet<WordTranslate> WordTranslates { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new WordConfig());
			modelBuilder.ApplyConfiguration(new WordAudioConfig());
			modelBuilder.ApplyConfiguration(new WordExampleConfig());
			modelBuilder.ApplyConfiguration(new WordPictureConfig());
			modelBuilder.ApplyConfiguration(new WordTranscriptionConfig());
			modelBuilder.ApplyConfiguration(new WordTranslateConfig());
			modelBuilder.ApplyConfiguration(new RoleConfig());
		}
	}
}
