
namespace GestorDeFilmes.Models
{
    public class Filme
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string PosterPath { get; set; }

        public string FullPosterUrl => $"https://image.tmdb.org/t/p/w500{PosterPath}";
    }

    public class FilmeResponse
    {
        public List<Filme> ListaFilme { get; set; }
    }
}
