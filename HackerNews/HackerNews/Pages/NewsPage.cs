using System.Collections;
using System.Linq;
using HackerNews.Shared;
using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Essentials;

namespace HackerNews
{
    public class NewsPage : BaseContentPage<NewsViewModel>
    {
        public NewsPage() : base("Top Stories")
        {
            ViewModel.PullToRefreshFailed += HandlePullToRefreshFailed;

            var collectionView = new CollectionView
                {
                    BackgroundColor = Color.FromHex("F6F6EF"),
                    SelectionMode = SelectionMode.Single,
                    ItemTemplate = new StoryDataTemplate()

                };

            collectionView.SetBinding(CollectionView.ItemsSourceProperty, nameof(NewsViewModel.TopStoryCollection));
            collectionView.SelectionChanged += HandleSelectionChanged;

            var refreshView = new RefreshView
            {
                RefreshColor = Colors.Black,
                Content = collectionView
            };

            refreshView.SetBinding(RefreshView.IsRefreshingProperty, nameof(NewsViewModel.IsListRefreshing));
            refreshView.SetBinding(RefreshView.CommandProperty, nameof(NewsViewModel.RefreshCommand));

            Content = refreshView;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Content is RefreshView refreshView
                && refreshView.Content is CollectionView collectionView
                && IsNullOrEmpty(collectionView.ItemsSource))
            {
                refreshView.IsRefreshing = true;
            }

            static bool IsNullOrEmpty(in IEnumerable? enumerable) => !enumerable?.GetEnumerator().MoveNext() ?? true;
        }

        async void HandleSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var collectionView = (CollectionView)sender;
            collectionView.SelectedItem = null;

            if (e.CurrentSelection.FirstOrDefault() is StoryModel storyModel)
            {
                if (!string.IsNullOrEmpty(storyModel.Url))
                {
                    var browserOptions = new BrowserLaunchOptions
                    {
                        PreferredControlColor = ColorConstants.BrowserNavigationBarTextColor,
                        PreferredToolbarColor = ColorConstants.BrowserNavigationBarBackgroundColor
                    };

                    await Browser.OpenAsync(storyModel.Url, browserOptions);
                }
                else
                {
                    await DisplayAlert("Invalid Article", "ASK HN articles have no url", "OK");
                }
            }
        }

        void HandlePullToRefreshFailed(object sender, string message) =>
            Device.BeginInvokeOnMainThread(async () => await DisplayAlert("Refresh Failed", message, "OK"));
    }
}
