using GestorDeFilmes.Models;
using System.Net.Http.Json;

namespace GestorDeFilmes.Core.Services
{
    public class TMDbService
    {
        private readonly HttpClient _httpClient;
        private const string UrlBase = "https://api.themoviedb.org/3/";

        public TMDbService() => _httpClient = new HttpClient();
       
        public async Task<List<Filme>> PesquisaFilmesAsync(string query)
        {
            string url = $"{UrlBase}search/movie?api_key={TMDbSettings.TMDBKEY}&query={query}&language=pt-BR";

            var response = await _httpClient.GetFromJsonAsync<FilmeResponse>(url);
            return response?.ListaFilme ?? new List<Filme>();
        }
    }
}
