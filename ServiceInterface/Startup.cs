using BusinessLogic.Contract;
using BusinessLogic.Engines;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ResourceAccess.Context;
using ResourceAccess.Engines;
using System.Text.Json.Serialization;

namespace MunicipalityTax.ServiceInterface
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddScoped<IResourceMunicipalityTaxScheduleEngine, ResourceMunicipalityTaxScheduleEngine>();
			services.AddScoped<IBusinessMunicipalityTaxScheduleRetrieverEngine, BusinessMunicipalityTaxScheduleRetrieverEngine>();
			services.AddScoped<IBusinessMunicipalityTaxScheduleUpdaterEngine, BusinessMunicipalityTaxScheduleUpdaterEngine>();
			string connectionString = Configuration.GetConnectionString("MunicipalityTax_SqlServer");
			; IServiceCollection serviceCollections = services.AddDbContext<MunicipalityTaxScheduleDbContext>(opt => opt.UseSqlServer(connectionString));
			services.AddControllers(opt =>
			  {
				  //TODO add exception filter  opt.Filters.Add(new HandleExceptionAttribute(typeof(Exception), HttpStatusCode.InternalServerError));
				  opt.EnableEndpointRouting = false;
			  })
			  .ConfigureApiBehaviorOptions(opt =>
			  {
				  // disable automatic mapping of 4xx status codes to ProblemDetails
				  //		opt.SuppressMapClientErrors = true;

				  //TODO  Register ValidationError handling via factory (before context.Result is set)
				  //opt.InvalidModelStateResponseFactory = ModelValidationErrorHandler.CreateBadRequestProblemDetails;
			  })
			  .AddJsonOptions(opt =>
			  {
				  //Enable proper enum string serialization
				  opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
			  });
			services.AddAuthentication("Bearer")
			   .AddJwtBearer("Bearer", options =>
			   {
				   options.Authority = "https://localhost:5001";

				   options.TokenValidationParameters = new TokenValidationParameters
				   {
					   ValidateAudience = false
				   };
			   });

			services.AddAuthorization(options =>
			{
				options.AddPolicy("SecureUpdatePolicy",
					policy =>
					{
						policy.RequireClaim("client_id", "taxconsumer");
						policy.RequireClaim("scope", "municipalitytaxapi");
					});
			});
			services.AddMvc();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
			public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
			{
				if (env.IsDevelopment())
				{
					app.UseDeveloperExceptionPage();
				}
				app.UseAuthentication();

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
