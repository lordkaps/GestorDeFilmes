using GestorDeFilmes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GestorDeFilmes.Core.Utils
{
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
