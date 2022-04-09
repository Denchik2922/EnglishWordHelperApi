using System.Collections.Generic;

namespace Models
{
	public class Word
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public WordTranscription Transcription { get; set; }
		public WordAudio Audio { get; set; }
		public ICollection<WordExample> Examples { get; set; }
		public ICollection<WordTranslate> Translates { get; set; }
		public ICollection<WordPicture> Pictures { get; set; }
	}
}
