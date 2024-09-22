namespace MovieApp.MVC.ViewModels.GenreVMs
{
    public record GenreGetVM(int Id, string Name, bool IsDeleted, DateTime CreatedAt, DateTime ModifiedAt);
}
