using System.Collections.Generic;

namespace EnglishWordHelperApi.Dtos
{
	public class WordDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public WordTranscriptionDto WordTranscription { get; set; }
		public ICollection<WordTranslateDto> WordTranslates { get; set; }
	}
}
