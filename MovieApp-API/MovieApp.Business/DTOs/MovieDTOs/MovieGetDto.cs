using MovieApp.Business.DTOs.MovieImageDTOs;
using MovieApp.Core.Entities;

namespace MovieApp.Business.DTOs.MovieDTOs
{
    public record MovieGetDto(int Id, string Title, string Desc, bool IsDeleted, DateTime CreatedAt, DateTime ModifiedAt, int GenreId, ICollection<MovieImageGetDto> MovieImages);
}
