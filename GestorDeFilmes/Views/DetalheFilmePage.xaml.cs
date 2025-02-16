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

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is DetalheFilmeViewModel viewModel)
            viewModel.OnAppearing();
    }
}