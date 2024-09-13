namespace PathFinder.WebApp.Models
{
    public class JsonViewModel<T>
    {
        public bool IsValid { get; set; }
        public T Value { get; set; }
        public string Html { get; set; }

    }
}
