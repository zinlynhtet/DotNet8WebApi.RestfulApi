namespace DotNet8WebApi.Models
{
    public class TokenOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Secret { get; set; }
    }
}
