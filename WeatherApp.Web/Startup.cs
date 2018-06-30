using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using WeatherApp.Common;
using WeatherApp.Web;
using WeatherApp.Web.Commands;

namespace WeatherApp.Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			string elasticSearchEndponint = configuration.GetSection("ElasticSearch").GetValue<string>("Endpoint");
			Log.Logger = new LoggerConfiguration()
			   .Enrich.FromLogContext()
			   .MinimumLevel.Debug()
			   .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticSearchEndponint))
			   {
				   MinimumLogEventLevel = LogEventLevel.Verbose,
				   AutoRegisterTemplate = true
			   })
			   .CreateLogger();

			this.Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			var container = services.BuildServiceProvider();
			var exceptionFilter = new WeatherAppExceptionFilter(container.GetService<ILoggerFactory>(), container.GetService<IHostingEnvironment>());

			services
				.AddServicesFromLocalDllContainers()
				.AddSingleton<IConfig, WeatherAppConfig>()
				.AddSingleton<ICitySearchCommand, CitySearchCommand>()
				.AddSingleton(serviceProvider => new Lazy<ICitySearchCommand>(() =>
					serviceProvider.GetRequiredService<ICitySearchCommand>()))
				.AddSingleton<IUKCityCommand, UKCityCommand>()
				.AddSingleton(serviceProvider => new Lazy<IUKCityCommand>(() =>
					serviceProvider.GetRequiredService<IUKCityCommand>()))
				.AddMemoryCache()
				.AddMvc()
				.AddMvcOptions(o => o.Filters.Add(exceptionFilter));
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddSerilog();
			app.UseStatusCodePagesWithReExecute("/Error/StatusError", "?status={0}");

			app
				.UseStaticFiles()
				.UseMvc(routes =>
				{
					routes.MapRoute(
						name: "default",
						template: "{controller=CurrentWeather}/{action=CitySearch}");
				});
		}
	}
}
