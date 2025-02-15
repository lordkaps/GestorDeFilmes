using GestorDeFilmes.Views;

namespace GestorDeFilmes
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (DeviceInfo.Platform == DevicePlatform.iOS)
                MainPage = new NavigationPage(new MainFlyoutPage());
            else
                MainPage = new NavigationPage(new MainTabbedPage()); 

            //MainPage = new AppShell();
        }
    }
}
