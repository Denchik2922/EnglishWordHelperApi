namespace Models
{
	public class WordPicture
	{
		public int Id { get; set; }
		public int WordId { get; set; }
		public Word Word { get; set; }
		public string PictureUrl { get; set; }
	}
}
