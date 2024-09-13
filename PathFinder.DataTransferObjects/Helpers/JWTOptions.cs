namespace STS.DataTransferObjects.Helpers
{
    public class JWTOptions
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }
        public double TokenLifeTime { get; set; }
    }
}
