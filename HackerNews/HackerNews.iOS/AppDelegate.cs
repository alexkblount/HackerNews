using UIKit;
using Foundation;
using Microsoft.Maui;

namespace HackerNews.iOS
{
    [Register(nameof(AppDelegate))]
    public partial class AppDelegate : MauiUIApplicationDelegate<Startup>
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			return base.FinishedLaunching(app, options);
		}
    }
}
