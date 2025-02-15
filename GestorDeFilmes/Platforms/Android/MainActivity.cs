using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using GestorDeFilmes.ViewModels;
using GestorDeFilmes.Views;

namespace GestorDeFilmes
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    [IntentFilter(new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
        DataScheme = "meuapp",
        DataHost = "callback")]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            var data = intent?.DataString;

            if (!string.IsNullOrEmpty(data))
            {
                OnAppLinkRequestReceived(intent.Data);
            }
        }

        private static void OnAppLinkRequestReceived(Android.Net.Uri uri)
        {
            if (uri != null)
            {
                var url = uri.ToString();
                if (Microsoft.Maui.Controls.Application.Current is App app)
                    if (App.Current.MainPage is NavigationPage page)
                        if (page.CurrentPage is MainTabbedPage mainTabbedPage)
                        {
                            if (mainTabbedPage.BindingContext is MainPageViewModel viewmodel)
                            {
                                viewmodel.TaskCompletion.SetResult(url);
                            }
                        }
                        else if (page.CurrentPage is MainFlyoutPage mainFlyoutPage)
                        {
                            if (mainFlyoutPage.BindingContext is MainPageViewModel viewmodel)
                            {
                                viewmodel.TaskCompletion.SetResult(url);
                            }
                        }

            }
        }
    }
}
