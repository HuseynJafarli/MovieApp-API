namespace MovieApp.Core.Entities
{
    public class Movie: BaseEntity
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
