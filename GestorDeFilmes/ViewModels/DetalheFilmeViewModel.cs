using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using GestorDeFilmes.Core.Utils;
using GestorDeFilmes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeFilmes.ViewModels
{
    public partial class DetalheFilmeViewModel : ObservableObject
    {
        [ObservableProperty]
        public Filme filme = new();

        public DetalheFilmeViewModel()
        {
            CarregaFilme();
        }

        private void CarregaFilme()
        {
            Parameter.Instance.TryGetParameter(nameof(DetalheFilmeViewModel), out object filmeParameter);
            if (filmeParameter != null && filmeParameter is Filme)
                Filme = (Filme)filmeParameter;
        }
    }
}
