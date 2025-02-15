﻿using GestorDeFilmes.Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

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
}
