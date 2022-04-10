using BLL.Services;
using BLL.Tests.Helpers;

namespace BLL.Tests.Fixtures
{
    public class WordServiceFixture
    {
        public WordService Create()
        {
            var context = new DBContextHelper().Context;
            var mapper = MapperHelper.GetInstance();
            return new WordService(context, mapper);
        }
    }
}
