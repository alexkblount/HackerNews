using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace HackerNews
{
    public class App : Microsoft.Maui.Controls.Application
    {
        public App()
        {

        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            var navigationPage = new Microsoft.Maui.Controls.NavigationPage(new NewsPage())
            {
                BarBackgroundColor = ColorConstants.NavigationBarBackgroundColor,
                BarTextColor = ColorConstants.NavigationBarTextColor
            };

            navigationPage.On<iOS>().SetPrefersLargeTitles(true);

        	return new Microsoft.Maui.Controls.Window(navigationPage);
        }
    }
}
