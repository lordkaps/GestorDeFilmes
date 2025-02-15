using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using GestorDeFilmes.Core.Utils;
using GestorDeFilmes.Models;
using GestorDeFilmes.Services;
using GestorDeFilmes.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GestorDeFilmes.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string textoLogin = "Efetuar Login";

        [ObservableProperty]
        private string searchText;

        [ObservableProperty]
        private bool deslogado = true;

        [ObservableProperty]
        public string tituloPersonalizado;

        [ObservableProperty]
        private List<Filme> listaFilme;

        [ObservableProperty]
        private List<Filme> listaFilmeFavorito;


        private readonly HttpClient _httpClient = new();
        private readonly TMDbService _tmdbService = new();
        private Usuario _usuarioLogado;
        private Sessao _sessao;

        public TaskCompletionSource<string> TaskCompletion;


        public MainPageViewModel()
        {
            CarregamentoInicial();
            ListaFilmeFavorito = DataBaseLocal.RecuperarListaDeFilmes();
        }

        private async void CarregamentoInicial()
        {
            try
            {
                ListaFilme = await _tmdbService.GetListaInicial();
                if (ListaFilme != null)
                    MarcaFavoritosSalvos();
            }
            catch (Exception ex)
            {
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", $"Ocorreu um erro: {ex.Message}", "OK");
                });
            }
        }

        [RelayCommand]
        private async Task Search(string busca)
        {
            //Não há necessidade de verificar se a string está vazia ou com espaço. O SearchBar só executa se possuir char.
            try
            {
                ListaFilme = await _tmdbService.PesquisaFilmesAsync(busca);
            }
            catch (Exception ex)
            {
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", $"Ocorreu um erro: {ex.Message}", "OK");
                });
            }
        }

        [RelayCommand]
        private async Task Logar()
        {
            try
            {
                await ExecutarLogin();
            }
            catch (Exception ex)
            {
                return;
            }
        }

        [RelayCommand]
        private async Task AbrirDetalhes(Filme filme)
        {
            if (filme == null)
                return;

            Parameter.Instance.AddParameter(nameof(DetalheFilmeViewModel), filme);

            await Application.Current!.MainPage!.Navigation.PushAsync(new DetalheFilmePage());
            //await Shell.Current.GoToAsync(nameof(DetalheFilmePage), true);
        }

        [RelayCommand]
        private async Task MarcarComoFavorito(Filme filme)
        {
            if (filme == null) return;
            filme.Favorito = !filme.Favorito;

            AtualizaListaFavorito();
        }

        private async Task ExecutarLogin()
        {
            TaskCompletion = new TaskCompletionSource<string>();
            await _tmdbService.OpenAuthPageAsync();
            var autorizacao = await TaskCompletion.Task;

            var token = _tmdbService.ExtraiaToken(autorizacao);
            _sessao = await _tmdbService.ObtenhaSessao(token);
            _usuarioLogado = await _tmdbService.ObtenhaUsuario(_sessao);

            Deslogado = false;
            TituloPersonalizado = $"{_usuarioLogado.NomeUsuario} Id: {_usuarioLogado.IDUsuario}";
            TextoLogin = "Deslogar";

            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await Application.Current.MainPage.DisplayAlert("Olaaaaa!!", $"Seja bem vindo ao aplicativo {_usuarioLogado.NomeUsuario}\r\n seu codigo de usuario é {_usuarioLogado.IDUsuario}", "ok");
            });
        }

        private void AtualizaListaFavorito()
        {
            ListaFilmeFavorito = ListaFilme.Where(f => f.Favorito).ToList();
            DataBaseLocal.SalvarListaDeFilmes(ListaFilmeFavorito);
            if (!Deslogado)
                SincronizaFavoritosComAPI();
        }

        private void MarcaFavoritosSalvos()
        {
            ListaFilme.ForEach(filme =>
            {
                if (ListaFilmeFavorito.Any(f => f.Id == filme.Id))
                {
                    filme.Favorito = true;
                }
            });
        }

        private void SincronizaFavoritosComAPI()
        {
            if (ListaFilmeFavorito != null && ListaFilmeFavorito.Count > 0) 
            {
                ListaFilmeFavorito.ForEach(async f => 
                {
                    await _tmdbService.AdicionarFilmeFavorito(_usuarioLogado.IDUsuario, f.Id, f.Favorito, _sessao.SessaoCode); 
                });
            }
        }
    }


}
