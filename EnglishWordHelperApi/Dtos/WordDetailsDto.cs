using System.Collections.Generic;

namespace EnglishWordHelperApi.Dtos
{
	public class WordDetailsDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public WordTranscriptionDto WordTranscription { get; set; }
		public WordAudioDto WordAudio { get; set; }
		public ICollection<WordExampleDto> WordExamples { get; set; }
		public ICollection<WordTranslateDto> WordTranslates { get; set; }
		public ICollection<WordPictureDto> WordPictures { get; set; }
	}
}
