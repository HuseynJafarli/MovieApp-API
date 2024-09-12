using MovieApp.Business.DTOs.GenreDTOs;
using MovieApp.Core.Entities;
using System.Linq.Expressions;

namespace MovieApp.Business.Services.Interfaces
{
    public interface IGenreService
    {
        Task CreateAsync(GenreCreateDto dto);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, GenreUpdateDto dto);
        Task<GenreGetDto> GetById(int id);
        Task<ICollection<GenreGetDto>> GetByExpression(bool asNoTracking = false, Expression<Func<Genre, bool>>? expression = null, params string[] includes);
        Task<GenreGetDto> GetSingleByExpression(bool asNoTracking = false, Expression<Func<Genre, bool>>? expression = null, params string[] includes);
    }
}
