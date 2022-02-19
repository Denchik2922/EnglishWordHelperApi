using System.Collections.Generic;

namespace Models
{
	public class Word
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int WordTranscriptionId { get; set; }
		public WordTranscription WordTranscription { get; set; }
		public int WordAudioId { get; set; }
		public WordAudio WordAudio { get; set; }
		public ICollection<WordExample> WordExamples { get; set; }
		public ICollection<WordTranslate> WordTranslates { get; set; }
		public ICollection<WordPicture> WordPictures { get; set; }
	}
}
