using EnglishWordHelperApi.Infrastructure.Validators;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EnglishWordHelperApi.Dtos
{
	public class WordDetailsDto
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Transcription is required")]
		public string Transcription { get; set; }
		public string UrlAudio { get; set; }

		[EnsureMinimumElements(1, ErrorMessage = "At least a Examples is required")]
		public ICollection<string> Examples { get; set; }

		[EnsureMinimumElements(1, ErrorMessage = "At least a Translates is required")]
		public ICollection<string> Translates { get; set; }
		public ICollection<string> Pictures { get; set; }
	}
}
