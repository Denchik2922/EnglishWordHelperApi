﻿namespace Models
{
	public class WordTranslate
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int WordId { get; set; }
		public Word Word { get; set; }
	}
}
