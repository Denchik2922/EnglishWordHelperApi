using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BLL.Tests.Helpers
{
    public class DBContextHelper: IDisposable
    {
        public EnglishContext Context { get; set; }
        public DBContextHelper()
        {
            var serviceProvicer = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<EnglishContext>()
                .UseInMemoryDatabase("UNIT_TESTING")
                .UseInternalServiceProvider(serviceProvicer);

            var options = builder.Options;
            Context = new EnglishContext(options);

            Context.AddRange(WordHelper.GetMany());

            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
