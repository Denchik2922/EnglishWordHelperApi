using AutoMapper;
using EnglishWordHelperApi.Dtos;
using Models;


namespace EnglishWordHelperApi.Infrastructure.Profiles
{
	public class WordTranscriptionProfile : Profile
    {
        public WordTranscriptionProfile()
        {
            CreateMap<WordTranscription, WordTranscriptionDto>();
            CreateMap<WordTranscriptionDto, WordTranscription>();
        }
    }
}
