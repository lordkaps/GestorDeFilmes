using GestorDeFilmes.Models;
using GestorDeFilmes.ViewModels;

namespace GestorDeFilmes.Views;

public partial class DetalheFilmePage : ContentPage
{
    public DetalheFilmePage()
	{
		InitializeComponent();
        BindingContext = new DetalheFilmeViewModel();
    }
}