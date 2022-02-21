using AutoMapper;
using EnglishWordHelperApi.Dtos;
using Models;

namespace EnglishWordHelperApi.Infrastructure.Profiles
{
	public class WordProfile : Profile
    {
        public WordProfile()
        {
            CreateMap<Word, Word>();
            CreateMap<Word, WordDto>();
            CreateMap<WordDto, Word>();
            CreateMap<Word, WordDetailsDto>();
            CreateMap<WordDetailsDto, Word>();
        }
    }
}
