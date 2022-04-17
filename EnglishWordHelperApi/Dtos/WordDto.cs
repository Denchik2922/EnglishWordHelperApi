using System.Collections.Generic;

namespace EnglishWordHelperApi.Dtos
{
	public class WordDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Transcription { get; set; }
		public ICollection<string> Translates { get; set; }
	}
}
