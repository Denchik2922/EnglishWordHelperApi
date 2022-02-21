using AutoMapper;
using EnglishWordHelperApi.Dtos;
using Models;


namespace EnglishWordHelperApi.Infrastructure.Profiles
{
	public class WordPictureProfile : Profile
    {
        public WordPictureProfile()
        {
            CreateMap<WordPicture, WordPictureDto>();
            CreateMap<WordPictureDto, WordPicture>();
        }
    }
}
