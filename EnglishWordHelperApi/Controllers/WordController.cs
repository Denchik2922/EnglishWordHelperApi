using AutoMapper;
using BLL.Interfaces;
using BLL.RequestFeatures;
using EnglishWordHelperApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishWordHelperApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WordController : ControllerBase
	{
        private readonly IMapper _mapper;
        private readonly IWordService _wordService;

        public WordController(IWordService wordService, IMapper mapper)
        {
            _wordService = wordService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<WordDto>>> GetAll([FromQuery] PaginationParameters parameters)
        {
            var words = await _wordService.GetAllAsync(parameters);

            ICollection<WordDto> wordsDto = _mapper.Map<ICollection<WordDto>>(words);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(words.MetaData));

            return Ok(wordsDto);
        }

        [HttpGet("get-information/{wordName}")]
        public async Task<ActionResult<WordDetailsDto>> GetByName(string wordName)
        {
            var word = await _wordService.GetWordByName(wordName);
            WordDetailsDto wordDto = _mapper.Map<WordDetailsDto>(word);

            return Ok(wordDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WordDetailsDto>> GetById(int id)
        {
            var word = await _wordService.GetByIdAsync(id);
            WordDetailsDto wordDto = _mapper.Map<WordDetailsDto>(word);

            return Ok(wordDto);
        }

        [HttpPost]
        public async Task<ActionResult> Add(WordDetailsDto wordDto)
        {
            if (ModelState.IsValid)
            {
                var word = _mapper.Map<Word>(wordDto);
               
                await _wordService.AddAsync(word);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            await _wordService.DeleteAsync(id);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update(WordDetailsDto wordDto)
        {
            if (ModelState.IsValid)
            {
                var word = _mapper.Map<Word>(wordDto);
                await _wordService.UpdateAsync(word);

                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
