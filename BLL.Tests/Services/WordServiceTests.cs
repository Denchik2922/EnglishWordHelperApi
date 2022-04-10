using BLL.RequestFeatures;
using BLL.Tests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BLL.Tests.Services
{
    public class WordServiceTests : IClassFixture<WordServiceFixture>
    {
        private readonly WordServiceFixture _fixture;
        public WordServiceTests(WordServiceFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Should_Return_All_Words()
        {
            //arrange
            var service = _fixture.Create();
            var paginParam = new PaginationParameters();

            //act
            var response = await service.GetAllAsync(paginParam);

            //assert
            Assert.Equal(2, response.Count);
        }

        [Fact]
        public async Task Should_Return_Word_By_Id()
        {
            //arrange
            var service = _fixture.Create();
            int wordId = 1;

            //act
            var response = await service.GetByIdAsync(wordId);

            //assert
            Assert.Equal(wordId, response.Id);
        }
    }
}
