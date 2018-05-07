using System.IO;
using MediaCloud.Common;
using MediaCloud.Domain;
using MediaCloud.Domain.Entities;
using MediaCloud.Domain.Repositories;
using MediaCloud.Domain.Repositories.Movie;
using MediaCloud.Domain.Repositories.Series;
using MediaCloud.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TMDbLib.Client;

namespace MediaCloud.Web {

  public class Startup {

    public static IConfiguration Configuration { get; set; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services) {
      services.AddMvc();

      services.AddDbContext<MediaCloudContext>();

      services.AddTransient(s => new TMDbClient(Settings.TmdbApiKey));
      services.AddTransient<IMovieApiRepository, TmdbMovieApiRepository>();
      services.AddTransient<ISeriesApiRepository, TmdbSeriesApiRepository>();

      services.AddTransient<IUnitOfWork, UnitOfWork>();

      services.AddTransient<ILibraryService<Library>, LibraryService>();
      services.AddTransient<ILibraryService<MovieLibrary>, MovieLibraryService>();
      services.AddTransient<ILibraryService<SeriesLibrary>, SeriesLibraryService>();
      services.AddTransient<IItemService<Movie>, MovieService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
      var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json");

      Configuration = builder.Build();

      app.Use(async (context, next) => {
        await next();
        if (context.Response.StatusCode == 404 &&
            !Path.HasExtension(context.Request.Path.Value) &&
            !context.Request.Path.Value.StartsWith("/api/")) {
          context.Request.Path = "/index.html";
          await next();
        }
      });
      app.UseMvcWithDefaultRoute();
      app.UseDefaultFiles();
      app.UseStaticFiles();
    }
  }
}
