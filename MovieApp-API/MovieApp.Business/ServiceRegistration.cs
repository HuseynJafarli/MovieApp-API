using Microsoft.Extensions.DependencyInjection;
using MovieApp.Business.Services.Implementations;
using MovieApp.Business.Services.Interfaces;

namespace MovieApp.Business
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IGenreService, GenreService>();
        }
    }
}
