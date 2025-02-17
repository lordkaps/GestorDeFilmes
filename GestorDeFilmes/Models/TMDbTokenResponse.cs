using System.Text.Json.Serialization;

namespace GestorDeFilmes.Models
{
    public class TMDbTokenResponse
    {
        [JsonPropertyName("request_token")]
        public string Request_token { get; set; }
    }
}
