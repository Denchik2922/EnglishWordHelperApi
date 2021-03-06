using BLL.Interfaces;
using BLL.Services;
using DAL;
using EnglishWordHelperApi.Infrastructure.Helpers;
using EnglishWordHelperApi.Infrastructure.Profiles;
using EnglishWordHelperApi.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models;
using Serilog;
using System.IO;
using System.Text;

namespace EnglishWordHelperApi
{
	public class Startup
	{
		public string ConnectionString { get; }
		public string JwtValidIssuer { get; }
		public string JwtValidAudience { get; }
		public string JwtSecret { get; }
		public string StaticFileFolder { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;

			ConnectionString = Configuration.GetConnectionString("DefaultConnection");

			JwtValidIssuer = Configuration["JWTSettings:validIssuer"];
			JwtValidAudience = Configuration["JWTSettings:validAudience"];
			JwtSecret = Configuration["JWTSettings:Secret"];

			StaticFileFolder = Configuration["StaticFilesOptions:MainFolder"];
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
			services.AddIdentity<AppUser, IdentityRole>()
			   .AddEntityFrameworkStores<EnglishContext>();

			services.Configure<IdentityOptions>(options =>
			{
				options.User.RequireUniqueEmail = true;
				options.User.AllowedUserNameCharacters = null;
			});

			//Configure jwt authentication
			services.AddAuthentication(opt =>
			{
				opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,

					ValidIssuer = JwtValidIssuer,
					ValidAudience = JwtValidAudience,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecret))
				};
			});

			//Services
			services.AddScoped<IWordService, WordService>();
			services.AddScoped<ITokenService, TokenService>();
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IUploadService, UploadService>();

			//AutoMapper profiles
			services.AddAutoMapper(typeof(WordProfile));

			services.AddCors();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<AppUser> userManager, ILoggerFactory loggerFactory)
		{
			DbInitializerHelper.SeedAdmins(userManager, Configuration);

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EnglishWordHelperApi v1"));
			}

			app.UseMiddleware<ExceptionMiddleware>(loggerFactory.CreateLogger(nameof(ExceptionMiddleware)));

			app.UseStaticFiles();
			app.UseStaticFiles(new StaticFileOptions()
			{
				FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), StaticFileFolder)),
				RequestPath = new PathString($"/{StaticFileFolder}")
			});

			app.UseHttpsRedirection();

			app.UseSerilogRequestLogging();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseCors(builder => builder.WithOrigins("https://localhost:5011")
							.AllowAnyMethod()
							.AllowAnyHeader()
							.WithExposedHeaders("X-Pagination"));

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
