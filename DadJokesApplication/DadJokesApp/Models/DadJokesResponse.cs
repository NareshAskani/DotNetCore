namespace DadJokesApp.Models
{
    public class DadJokesResponse<T>
    {
        public bool Success { get; set; }
        public T Body { get; set; }
    }
}
