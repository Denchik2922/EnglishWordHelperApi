using AutoMapper;
using EnglishWordHelperApi.Dtos;
using Models;
using System.Linq;

namespace EnglishWordHelperApi.Infrastructure.Profiles
{
	public class WordProfile : Profile
    {
        public WordProfile()
        {
            CreateMap<Word, Word>();

            CreateMap<Word, WordDto>()
                .ForMember(wordDto => wordDto.Transcription, opt => opt
                    .MapFrom(word => word.Transcription.Name));

            CreateMap<Word, WordDetailsDto>()
                .ForMember(wordDto => wordDto.Transcription, opt => opt
                    .MapFrom(word => word.Transcription.Name))
                .ForMember(wordDto => wordDto.UrlAudio, opt => opt
                    .MapFrom(word => word.Audio.AudioUrl));

            CreateMap<Word, WordForUserDto>()
               .ForMember(wordDto => wordDto.Transcription, opt => opt
                   .MapFrom(word => word.Transcription.Name))
               .ForMember(wordDto => wordDto.UrlAudio, opt => opt
                   .MapFrom(word => word.Audio.AudioUrl))
               .ForMember(wordDto => wordDto.Translates, opt => opt
                   .MapFrom(word => word.Translates.Select(t => t.Name)))
               .ForMember(wordDto => wordDto.Examples, opt => opt
                   .MapFrom(word => word.Examples.Select(e => e.Example)))
               .ForMember(wordDto => wordDto.Pictures, opt => opt
                   .MapFrom(word => word.Pictures.Select(p => p.PictureUrl)));


            CreateMap<WordDetailsDto, Word>()
                .ForMember(word => word.Transcription, opt => opt
                    .MapFrom(wordDto => new WordTranscription { Name = wordDto.Transcription ?? string.Empty }))
                .ForMember(word => word.Audio, opt => opt
                    .MapFrom(wordDto => new WordAudio { AudioUrl = wordDto.UrlAudio ?? string.Empty}));
        }
    }
}
