using Foundation;
using GestorDeFilmes.Services.Interfaces;
using UIKit;

namespace GestorDeFilmes.Platforms.iOS
{
    public class ShareService : IShareService
    {

        /// <summary>
        /// Forma nativa para compartilhamento no iOS
        /// </summary>
        public async Task ShareText(string text)
        {
            var items = new NSObject[] { new NSString(text) };
            var activityController = new UIActivityViewController(items, null);

            var rootController = UIApplication.SharedApplication.KeyWindow.RootViewController;
            rootController.PresentViewController(activityController, true, null);
        }
    }
}
