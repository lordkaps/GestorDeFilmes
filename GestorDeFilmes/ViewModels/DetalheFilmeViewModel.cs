using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using GestorDeFilmes.Core.Utils;
using GestorDeFilmes.Models;
using GestorDeFilmes.Services.Interfaces;

namespace GestorDeFilmes.ViewModels
{
    public partial class DetalheFilmeViewModel : ObservableObject
    {
        #region Propriedades
        [ObservableProperty]
        public Filme filme = new();

        [ObservableProperty]
        private string lancamento;

        private readonly IShareService _shareService;
        #endregion

        #region Inicial
        public DetalheFilmeViewModel(IShareService shareService)
        {
            _shareService = shareService;
        }

        public async void OnAppearing()
        {
            CarregaFilme();
        }

        private void CarregaFilme()
        {
            Parameter.Instance.TryGetParameter(nameof(DetalheFilmeViewModel), out object filmeParameter);
            if (filmeParameter != null && filmeParameter is Filme)
            {
                Filme = (Filme)filmeParameter;
                Lancamento = Filme.LancamentoFormatado;
            }
        }
        #endregion

        #region Commands
        [RelayCommand]
        private async Task Compartilhar()
        {
            bool nativo = await Application.Current.MainPage.DisplayAlert("Compartilhamento", "Qual método de compartilhamento?", "Nativo", "Padrão");

            if (nativo)
                await CompartilharFilmeNativoAsync();
            else
                await CompartilharFilmeAsync();
        }
        #endregion

        #region Funções
        private async Task CompartilharFilmeAsync()
        {
            if (Filme == null) return;

            try
            {
                string mensagem = $"🎬 *{Filme.Title}*\n📅 Lançamento: {Filme.ReleaseDate}\n📖 {Filme.Overview}\n🔗 Mais detalhes: https://www.themoviedb.org/movie/{Filme.Id}";

                string localFilePath = await BaixarImagemAsync(Filme.FullPosterUrl);
                if (string.IsNullOrEmpty(localFilePath))
                {
                    await Share.Default.RequestAsync(new ShareTextRequest
                    {
                        Text = mensagem,
                        Title = "Compartilhar Filme"
                    });
                    return;
                }

                await Share.Default.RequestAsync(new ShareFileRequest
                {
                    Title = mensagem,
                    File = new ShareFile(localFilePath),

                });
            }
            catch (Exception ex)
            {
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", $"Ocorreu um erro: {ex.Message}", "OK");
                });
            }
        }

        private async Task CompartilharFilmeNativoAsync()
        {
            await _shareService.ShareText($"🎬 *{Filme.Title}*\n📅 Lançamento: {Filme.ReleaseDate}\n📖 {Filme.Overview}\n🔗 Mais detalhes: https://www.themoviedb.org/movie/{Filme.Id}");
        }

        private async Task<string> BaixarImagemAsync(string imageUrl)
        {
            try
            {
                using var httpClient = new HttpClient();
                var imageBytes = await httpClient.GetByteArrayAsync(imageUrl);

                if (imageBytes == null || imageBytes.Length == 0)
                    return string.Empty;

                string filePath = Path.Combine(FileSystem.CacheDirectory, "filme_compartilhado.jpg");

                await File.WriteAllBytesAsync(filePath, imageBytes);
                return filePath;
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion
    }
}
