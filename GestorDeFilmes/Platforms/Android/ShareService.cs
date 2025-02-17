using Android.Content;

using GestorDeFilmes.Services.Interfaces;

using Application = Android.App.Application;

namespace GestorDeFilmes.Platforms.Android
{
    public class ShareService : IShareService
    {
        /// <summary>
        /// Compartilhamento nativo android
        /// </summary>
        public async Task ShareText(string text)
        {
            var intent = new Intent(Intent.ActionSend);
            intent.SetType("text/plain");
            intent.PutExtra(Intent.ExtraText, text);

            var chooserIntent = Intent.CreateChooser(intent, "Compartilhar via");
            chooserIntent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);

            Application.Context.StartActivity(chooserIntent);
        }
    }
}
