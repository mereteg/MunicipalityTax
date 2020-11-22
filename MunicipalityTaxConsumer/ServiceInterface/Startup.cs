using BusinessLogic.Contract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ResourceAccess.Engines;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using System.Net.Http;

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
			services.AddScoped<HttpClient>();
			services.AddScoped<ISimpleRestClient, SimpleRestClient>();
		
		    services.AddControllers(opt =>
			  {
				//TODO add exception filter  opt.Filters.Add(new HandleExceptionAttribute(typeof(Exception), HttpStatusCode.InternalServerError));
				  opt.EnableEndpointRouting = false;
			  })
			  .ConfigureApiBehaviorOptions(opt =>
			  {
					// disable automatic mapping of 4xx status codes to ProblemDetails
					opt.SuppressMapClientErrors = true;

					//TODO  Register ValidationError handling via factory (before context.Result is set)
					//opt.InvalidModelStateResponseFactory = ModelValidationErrorHandler.CreateBadRequestProblemDetails;
			  })
			  .AddJsonOptions(opt =>
			  {
					//Enable proper enum string serialization
					opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
			  });

			services.AddMvc();

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tax Consumer API", Version = "v1" });
			});

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			});

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
