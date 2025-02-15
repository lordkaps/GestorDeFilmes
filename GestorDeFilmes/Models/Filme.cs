
using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json.Serialization;

namespace GestorDeFilmes.Models
{
    public class Filme
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; }

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }

        public bool Favorito { get; set; }
        public string FullPosterUrl => $"https://image.tmdb.org/t/p/w500{PosterPath}";

        public Color CorFavorito => Favorito ? Colors.Yellow : Colors.Gray;
    }

    public class FilmeResponse : ObservableObject
    {
        [JsonPropertyName("page")]
        public int Page {  get; set; }

        [JsonPropertyName("results")]
        public List<Filme>? Results { get; set; } = new();

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("total_results")]
        public int TotalResults { get; set; }
    }
}
