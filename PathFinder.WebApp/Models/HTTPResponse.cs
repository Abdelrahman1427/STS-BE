namespace PathFinder.WebApp.Models
{
    public class HttpResponse<T>: JsonResponse
    {
        public T Value { get; set; }
    }
}
