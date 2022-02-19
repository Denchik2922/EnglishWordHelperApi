namespace Models
{
	public class WordAudio
	{
		public int Id { get; set; }
		public int WordId { get; set; }
		public Word Word { get; set; }
		public string AudioUrl { get; set; }
	}
}
