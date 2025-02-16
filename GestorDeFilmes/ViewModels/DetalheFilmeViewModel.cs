using CommunityToolkit.Mvvm.ComponentModel;
using GestorDeFilmes.Core.Utils;
using GestorDeFilmes.Models;

namespace GestorDeFilmes.ViewModels
{
    public partial class DetalheFilmeViewModel : ObservableObject
    {
        [ObservableProperty]
        public Filme filme = new();

        public DetalheFilmeViewModel(){}

        public async void OnAppearing()
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
