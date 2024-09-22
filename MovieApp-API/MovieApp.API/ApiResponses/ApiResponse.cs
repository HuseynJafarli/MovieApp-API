namespace MovieApp.API.ApiResponses
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }
        public bool IsSuccessfull => StatusCode >= 200 && StatusCode < 300 ? true : false;
    }
}
