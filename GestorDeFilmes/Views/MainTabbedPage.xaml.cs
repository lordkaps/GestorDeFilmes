using GestorDeFilmes.Models;
using GestorDeFilmes.ViewModels;
using System.IO;

namespace GestorDeFilmes.Views;

public partial class MainTabbedPage : TabbedPage
{
    public MainTabbedPage()
	{
		InitializeComponent();
        BindingContext = new MainPageViewModel();
    }

    private void OnFavoritoClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is Filme filme)
        {
            // Alterar cor do botão com base no estado de favorito
            button.TextColor = filme.Favorito ? Colors.Yellow : Colors.Gray;
        }
    }
}