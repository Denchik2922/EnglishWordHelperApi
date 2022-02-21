using AutoMapper;
using EnglishWordHelperApi.Dtos;
using Models;

namespace EnglishWordHelperApi.Infrastructure.Profiles
{
	public class WordAudioProfile : Profile
    {
        public WordAudioProfile()
        {
            CreateMap<WordAudio, WordAudioDto>();
            CreateMap<WordAudioDto, WordAudio>();
        }
    }
}
