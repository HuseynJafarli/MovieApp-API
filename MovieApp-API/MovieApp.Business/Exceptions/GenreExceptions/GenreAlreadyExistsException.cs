namespace MovieApp.Business.Exceptions.GenreExceptions
{
    public class GenreAlreadyExistsException: Exception
    {
        public int StatusCode { get; set; }
        public string PropertyName { get; set; }
        public GenreAlreadyExistsException()
        {
        }

        public GenreAlreadyExistsException(string? message) : base(message)
        {
        }

        public GenreAlreadyExistsException(int statusCode, string propertyName, string? message) : base(message)
        {
            StatusCode = statusCode;
            PropertyName = propertyName;
        }
    }
}
