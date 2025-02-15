using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GestorDeFilmes.Models
{
    public class TMDbTokenResponse
    {
        [JsonPropertyName("request_token")]
        public string Request_token { get; set; }
    }
}
