using GestorDeFilmes.Services.Interfaces;
using GestorDeFilmes.ViewModels;

namespace GestorDeFilmes.Views;

public partial class DetalheFilmePage : ContentPage
{
    public DetalheFilmePage(IShareService shareService)
	{
		InitializeComponent();
        BindingContext = new DetalheFilmeViewModel(shareService);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is DetalheFilmeViewModel viewModel)
            viewModel.OnAppearing();
    }
}