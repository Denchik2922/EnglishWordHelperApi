using System.Collections.Generic;

namespace EnglishWordHelperApi.Dtos
{
	public class WordDetailsDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Transcription { get; set; }
		public string UrlAudio { get; set; }
		public ICollection<WordExampleDto> Examples { get; set; }
		public ICollection<WordTranslateDto> Translates { get; set; }
		public ICollection<WordPictureDto> Pictures { get; set; }
	}
}
