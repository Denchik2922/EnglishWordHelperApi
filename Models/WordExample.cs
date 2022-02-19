namespace Models
{
	public class WordExample
	{
		public int Id { get; set; }
		public string Example { get; set; }
		public int WordId { get; set; }
		public Word Word { get; set; }
	}
}
