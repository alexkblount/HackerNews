using HackerNews.Shared;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace HackerNews
{
    public class StoryDataTemplate : DataTemplate
    {
        public StoryDataTemplate() : base(CreateGrid)
        {

        }

        public static GridLength AbsoluteGridLength(double value) => new GridLength(value, GridUnitType.Absolute);

        static Grid CreateGrid()
        {
            var title = new TitleLabel();
            title.SetBinding(Label.TextProperty, nameof(StoryModel.Title));
            Grid.SetRow(title, 0);

            var description = new DescriptionLabel();
            description.SetBinding(Label.TextProperty, nameof(StoryModel.Description));
            Grid.SetRow(description, 1);

            var definitions = new RowDefinitionCollection();
            definitions.Add(new RowDefinition { Height = new GridLength(20, GridUnitType.Absolute)});
            definitions.Add(new RowDefinition { Height = new GridLength(20, GridUnitType.Absolute)});
            definitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Absolute)});
            var grid = new Grid {
                RowSpacing = 1,
                Children =
            {
                title,
                description
            },
            RowDefinitions = definitions
            };

            return grid;
        }

        enum Row { Title, Description, BottomPadding }

        class TitleLabel : Label
        {
            public TitleLabel()
            {
                FontSize = 16;
                TextColor = ColorConstants.TextCellTextColor;

                VerticalTextAlignment = TextAlignment.Start;

                Padding = new Thickness(10, 0);
            }
        }

        class DescriptionLabel : Label
        {
            public DescriptionLabel()
            {
                FontSize = 13;
                TextColor = ColorConstants.TextCellDetailColor;

                Padding = new Thickness(10, 0, 10, 5);
            }
        }
    }
}