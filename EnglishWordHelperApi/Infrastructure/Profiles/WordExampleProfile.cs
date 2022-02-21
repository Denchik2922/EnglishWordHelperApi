using AutoMapper;
using EnglishWordHelperApi.Dtos;
using Models;


namespace EnglishWordHelperApi.Infrastructure.Profiles
{
	public class WordExampleProfile : Profile
    {
        public WordExampleProfile()
        {
            CreateMap<WordExample, WordExampleDto>();
            CreateMap<WordExampleDto, WordExample>();
        }
    }
}
