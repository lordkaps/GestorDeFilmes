using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

using GestorDeFilmes.Models;

using static Android.Gms.Common.Apis.Api;

namespace GestorDeFilmes.Services
{
    public class TMDbService
    {
        private readonly HttpClient _httpClient;

        public TMDbService() => _httpClient = new HttpClient();

        public async Task<List<Filme>> PesquisaFilmesAsync(string query)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(TMDbSettings.UrlBase + TMDbSettings.BuscaFilme + query + TMDbSettings.PadraoDeBusca),
                Headers =
                            {
                                { "accept", "application/json" },
                                { "Authorization", TMDbSettings.Bearer },
                            },
            };

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var movieResponse = JsonSerializer.Deserialize<FilmeResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                List<Filme> ListaFilme = movieResponse?.Results ?? new List<Filme>();
                return ListaFilme;
            }

            return new List<Filme>();
        }

        public async Task<List<Filme>> GetListaInicial()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(TMDbSettings.UrlBase + TMDbSettings.UrlListaPopular),
                Headers =
                            {
                                { "accept", "application/json" },
                                { "Authorization", TMDbSettings.Bearer },
                            },
            };

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var movieResponse = JsonSerializer.Deserialize<FilmeResponse>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});

                List<Filme> ListaFilme = movieResponse?.Results ?? new List<Filme>();
                return ListaFilme;
            }

            return new List<Filme>();
        }

        public async Task<string> GetRequestTokenAsync()
        {
            var apikeyWithURL = $"https://api.themoviedb.org/3/authentication/token/new?api_key={TMDbSettings.ApiKey}";
            var response = await _httpClient.GetStringAsync(apikeyWithURL);
            var json = JsonSerializer.Deserialize<TMDbTokenResponse>(response);
            return json?.Request_token;
        }

        public async Task OpenAuthPageAsync()
        {
            string requestToken = await GetRequestTokenAsync();
            string authUrl = $"https://www.themoviedb.org/authenticate/{requestToken}?redirect_to=meuapp://callback";

            await Launcher.OpenAsync(authUrl);
        }

        internal async Task<Sessao> ObtenhaSessao(string url)
        {
            var validateUrl = $"https://api.themoviedb.org/3/authentication/session/new?api_key={TMDbSettings.ApiKey}";

            var jsonContent = JsonSerializer.Serialize(new
            {
                request_token = url
            });

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");


            var response = await _httpClient.PostAsync(validateUrl, content);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(responseBody))
                {
                    return JsonSerializer.Deserialize<Sessao>(responseBody);

                }
                throw new Exception("Nao foi possivel iniciar sessao");
            }
            else
            {
                throw new Exception("Falha na validação do token");
            }
        }

        internal async Task<Usuario> ObtenhaUsuario(Sessao sessao)
        {
            var endPoint = $"https://api.themoviedb.org/3/account?api_key={TMDbSettings.ApiKey}&session_id={sessao.SessaoCode}";
            var response = await _httpClient.GetStringAsync(endPoint);
            return JsonSerializer.Deserialize<Usuario>(response);
        }

        public string ExtraiaToken(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri systemUri))
            {
                var queryParams = System.Web.HttpUtility.ParseQueryString(systemUri.Query);
                return queryParams["request_token"];
            }
            return null;
        }

        public async Task<bool> AdicionarFilmeFavorito(int accountId, int filmeId, bool favorito, string sessionId)
        {
            var url = $"{TMDbSettings.UrlBase}account/{accountId}/favorite?session_id={sessionId}";

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url),
                Headers =
                {
                    { "accept", "application/json" },
                    { "Authorization", $"{TMDbSettings.Bearer}"},
                },
                Content = new StringContent($"{{\"media_type\":\"movie\",\"media_id\":{filmeId},\"favorite\":true}}")
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/json")
                    }
                }
            };

            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
        }

        public async Task<List<Filme>> GetListFilmeFavorito(int accountId, string sessionId)
        {
            var url = $"{TMDbSettings.UrlBase}account/{accountId}/favorite/movies?language=pt-BR&page=1&session_id={sessionId}&sort_by=created_at.asc";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url), //"https://api.themoviedb.org/3/account/21814271/favorite/movies?language=pt-BR&page=1&session_id=1515151&sort_by=created_at.asc"),
                Headers =
                {
                    { "accept", "application/json" },
                    { "Authorization", $"{TMDbSettings.Bearer}"},
                },
            };

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var movieResponse = JsonSerializer.Deserialize<FilmeResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                List<Filme> ListaFilme = movieResponse?.Results ?? new List<Filme>();
                return ListaFilme;
            }

            return new List<Filme>();
        }
    }

    public class Sessao
    {
        [JsonPropertyName("success")]
        public bool Sucesso { get; set; }

        [JsonPropertyName("session_id")]
        public string SessaoCode { get; set; }

    }

    public class Usuario
    {
        [JsonPropertyName("id")]
        public int IDUsuario { get; set; }
        [JsonPropertyName("username")]
        public string NomeUsuario { get; set; }
    }


    /*
     var request = new HttpRequestMessage
{
    Method = HttpMethod.Post,
    RequestUri = new Uri("https://api.themoviedb.org/3/account/21814271/favorite?session_id=0a23941ffc44dd77a039b4a0231517bd7a1e842e"),
    Headers =
    {
        { "accept", "application/json" },
        { "Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI1ZWI1OGMxYTJhYWE1YTJkZmU3NmUyNTYzZTUzZmU0YSIsIm5iZiI6MTczOTM5Mjc2NC40MzgwMDAyLCJzdWIiOiI2N2FkMDZmY2Q2M2U5ZGVlZWEzNmQ1OWQiLCJzY29wZXMiOlsiYXBpX3JlYWQiXSwidmVyc2lvbiI6MX0.Jl2shUN7T8SRHuJMopNrVsOkGnmvxW395vpuQVmYD10" },
    },
    Content = new StringContent("{\"media_type\":\"movie\",\"media_id\":939243,\"favorite\":true}")
    {
        Headers =
        {
            ContentType = new MediaTypeHeaderValue("application/json")
        }
    }
};
using (var response = await client.SendAsync(request))
{
    response.EnsureSuccessStatusCode();
    var body = await response.Content.ReadAsStringAsync();
    Console.WriteLine(body);
}
     */
}
