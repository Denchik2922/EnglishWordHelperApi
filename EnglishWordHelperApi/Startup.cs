using BLL.Interfaces;
using BLL.Services;
using DAL;
using EnglishWordHelperApi.Infrastructure.Helpers;
using EnglishWordHelperApi.Infrastructure.Profiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace EnglishWordHelperApi
{
	public class Startup
	{
		public string ConnectionString { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;

			ConnectionString = Configuration.GetConnectionString("DefaultConnection");
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "EnglishWordHelperApi", Version = "v1" });
			});

			//DB Connection
			services.AddDbContext<EnglishContext>(options =>
			   options.UseSqlServer(ConnectionString));

			//Identity setting
			services.AddIdentity<IdentityUser, IdentityRole>()
			   .AddEntityFrameworkStores<EnglishContext>();

			services.Configure<IdentityOptions>(options =>
			{
				options.User.RequireUniqueEmail = true;
				options.User.AllowedUserNameCharacters = null;
			});

			//Services
			services.AddScoped<IWordService, WordService>();

			//AutoMapper profiles
			services.AddAutoMapper(typeof(WordProfile),
								   typeof(WordAudioProfile),
								   typeof(WordPictureProfile),
								   typeof(WordExampleProfile),
								   typeof(WordTranscriptionProfile),
								   typeof(WordTranslateProfile));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<IdentityUser> userManager)
		{
			DbInitializerHelper.SeedAdmins(userManager, Configuration);

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EnglishWordHelperApi v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}