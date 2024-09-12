using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;
using MovieApp.Data.Contexts;

namespace MovieApp.Data.Repositories
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        public MovieRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
