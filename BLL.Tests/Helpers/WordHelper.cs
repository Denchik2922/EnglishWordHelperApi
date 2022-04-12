using Models;
using System.Collections.Generic;

namespace BLL.Tests.Helpers
{
    public class WordHelper
    {
        public static Word GetOne(int id)
        {
            return new Word()
            {
                Id = id,
                Name = $"Word {id}",
            };
        }

        public static IEnumerable<Word> GetMany()
        {
            yield return GetOne(1);
            yield return GetOne(2);
        }
    }
}
