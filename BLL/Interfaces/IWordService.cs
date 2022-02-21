using BLL.RequestFeatures;
using Models;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
	public interface IWordService
	{
		Task AddAsync(Word word);
		Task DeleteAsync(int id);
		Task<PagedList<Word>> GetAllAsync(PaginationParameters parameters);
		Task<Word> GetByIdAsync(int id);
		Task<Word> GetWordByName(string wordName);
		Task UpdateAsync(Word word);
	}
}
