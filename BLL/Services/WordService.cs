using AutoMapper;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.RequestFeatures;
using DAL;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
	public class WordService : IWordService
	{
		private readonly EnglishContext _context;
		private readonly IMapper _mapper;

		public WordService(EnglishContext context, IMapper mapper)
		{
			_mapper = mapper;
			_context = context;
		}

		public async Task<PagedList<Word>> GetAllAsync(PaginationParameters parameters)
		{
			var words = GetWordQueryable();

			return await PagedList<Word>
						 .ToPagedList(words, parameters.PageNumber, parameters.PageSize);
		}

		public async Task<Word> GetByIdAsync(int id)
		{
			var word = await GetWordWithAllIncludesQueryable()
								.FirstOrDefaultAsync(w => w.Id == id);

			if (word == null)
			{
				throw new ItemNotFoundException($"{typeof(Word).Name} with id {id} not found");
			}

			return word;
		}

		public async Task<Word> GetWordByName(string wordName)
		{
			var word = await GetWordWithAllIncludesQueryable()
								.FirstOrDefaultAsync(w => w.Name.Contains(wordName));

			if (word == null)
			{
				throw new ItemNotFoundException($"{typeof(Word).Name} with name {wordName} not found");
			}

			return word;
		}

		private IQueryable<Word> GetWordQueryable()
		{
			return _context.Words
						   .Include(w => w.Transcription)
						   .Include(w => w.Translates);
		}

		private IQueryable<Word> GetWordWithAllIncludesQueryable()
		{
			return _context.Words
						   .Include(w => w.Audio)
						   .Include(w => w.Transcription)
						   .Include(w => w.Translates)
						   .Include(w => w.Examples)
						   .Include(w => w.Pictures)
						   .AsSplitQuery();
		}

		public async Task AddAsync(Word word)
		{
			await _context.Words.AddAsync(word);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var word = await GetByIdAsync(id);
			if (word == null)
			{
				throw new ItemNotFoundException($"{typeof(Word).Name} with id {id} not found");
			}

			_context.Remove(word);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Word word)
		{
			var oldWord = await GetByIdAsync(word.Id);

			_mapper.Map(word, oldWord);

			_context.Entry(oldWord).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}
	}
}
