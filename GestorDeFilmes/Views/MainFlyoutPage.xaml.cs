using GestorDeFilmes.ViewModels;

namespace GestorDeFilmes.Views;

public partial class MainFlyoutPage : FlyoutPage
{
	public MainFlyoutPage()
	{
		InitializeComponent();
		BindingContext = new MainPageViewModel();
	}
}