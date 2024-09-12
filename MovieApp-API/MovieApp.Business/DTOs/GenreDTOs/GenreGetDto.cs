namespace MovieApp.Business.DTOs.GenreDTOs
{
    public record GenreGetDto(int Id, string Name, bool IsDeleted, DateTime CreatedAt, DateTime ModifiedAt);
}
