using AutoMapper;
using EnglishWordHelperApi.Dtos;
using Models;

namespace EnglishWordHelperApi.Infrastructure.Profiles
{
	public class WordTranslateProfile : Profile
    {
        public WordTranslateProfile()
        {
            CreateMap<WordTranslate, WordTranslateDto>();
            CreateMap<WordTranslateDto, WordTranslate>();
        }
    }
}
