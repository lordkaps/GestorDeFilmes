using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

using AndroidX.Core.App;
using AndroidX.Core.Content;

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

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
                CreateNotificationChannel();
        }

        /// <summary>
        /// Cria Channel para as notificações push
        /// </summary>
        private void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                return;
            }

            if ((int)Build.VERSION.SdkInt > 29)
            {
                // Android 12 ao instalar já libera a permissão de notificacao. Ao destivar manulamente
                // não apresenta popup para permissão. Devendo usar as proprias configuracoes do android.
                if (ContextCompat.CheckSelfPermission(this, "android.permission.POST_NOTIFICATIONS") != Permission.Granted)
                {
                    ActivityCompat.RequestPermissions(this, new String[] { "android.permission.POST_NOTIFICATIONS" }, 1);
                }

                var channelName = "gestordefilmes";
                var channelDescription = "Notificação";
                var channel = new NotificationChannel(channelName, channelName, NotificationImportance.Default)
                {
                    Description = channelDescription
                };

                var notificationManager = (NotificationManager)GetSystemService(NotificationService);
                notificationManager.CreateNotificationChannel(channel);
            }
        }

        /// <summary>
        /// Quando recebe o retorno do link de autenticação;
        /// </summary>
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
