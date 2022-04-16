using System.Collections.Generic;

namespace EnglishWordHelperApi.Dtos
{
    public class WordForUserDto
    {
		public string Name { get; set; }
		public string Transcription { get; set; }
		public string UrlAudio { get; set; }
		public ICollection<string> Examples { get; set; }
		public ICollection<string> Translates { get; set; }
		public ICollection<string> Pictures { get; set; }
	}
}
