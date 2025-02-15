
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




/*
 {  
  "poster_path": "/1E5baAaEse26fej7uHcjOgEE2t2.jpg",  
  "adult": false,  
  "overview": "Jack Reacher must uncover the truth behind a major government conspiracy in order to clear his name. On the run as a fugitive from the law, Reacher uncovers a potential secret from his past that could change his life forever.",  
  "release_date": "2016-10-19",  
  "genre_ids": [  
    53,  
    28,  
    80,  
    18,  
    9648  
  ],  
  "id": 343611,  
  "original_title": "Jack Reacher: Never Go Back",  
  "original_language": "en",  
  "title": "Jack Reacher: Never Go Back",  
  "backdrop_path": "/4ynQYtSEuU5hyipcGkfD6ncwtwz.jpg",  
  "popularity": 26.818468,  
  "vote_count": 201,  
  "video": false,  
  "vote_average": 4.19  
}
 */
