using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;
using MovieApp.Data.Contexts;

namespace MovieApp.Data.Repositories
{
    public class MovieImageRepository : GenericRepository<MovieImage>, IMovieImageRepository
    {
        public MovieImageRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
