using Models;
using System.Linq;

namespace BLL.Extensions
{
    public static class WordServiceExtension
    {
        public static IQueryable<Word> Search(this IQueryable<Word> list, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return list;
            }
            var lowerCaseSearchTerm = searchTerm.Trim().ToLowerInvariant();

            return list.Where(d => d.Name.ToLower().Contains(lowerCaseSearchTerm));
        }
    }
}
