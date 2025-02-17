using GestorDeFilmes.Services.Interfaces;

using Plugin.FirebasePushNotifications;

namespace GestorDeFilmes
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseFirebasePushNotifications()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("fa-regular-400.ttf", "FontAwesome");
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if ANDROID
            builder.Services.AddSingleton<IShareService, Platforms.Android.ShareService>();
#elif IOS
        builder.Services.AddSingleton<IShareService, Platforms.iOS.ShareService>();
#endif

            return builder.Build();
        }
    }
}
