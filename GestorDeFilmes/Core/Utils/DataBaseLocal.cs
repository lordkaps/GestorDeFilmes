using System.Text.Json;

using GestorDeFilmes.Models;

namespace GestorDeFilmes.Core.Utils
{   
    /// <summary>
    /// Classe estatica criada para salvar de forma simples e localmente os filmes favoritos.
    /// </summary>
    public static class DataBaseLocal
    {
        public static void SalvarListaDeFilmes(List<Filme> filmes)
        {
            string filmesJson = JsonSerializer.Serialize(filmes);
            Preferences.Set("listaFilmes", filmesJson);
        }

        public static List<Filme> RecuperarListaDeFilmes()
        {
            string filmesJson = Preferences.Get("listaFilmes", string.Empty);
            if (string.IsNullOrEmpty(filmesJson))
                return new List<Filme>(); 

            return JsonSerializer.Deserialize<List<Filme>>(filmesJson);
        }
    }
}
