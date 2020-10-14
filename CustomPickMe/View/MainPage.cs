using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using CustomPicker;

namespace CustomPickMeMain
{
    public class MainPage : ContentPage
    {
        private Frame popupFrame;
        private AbsoluteLayout outerLayout;

        public MainPage()
        {
            outerLayout = new AbsoluteLayout
            {
                Padding = new Thickness(50)
            };

            StackLayout listStackLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.White
            };

            ListView listView = new ListView
            {
                Footer = "",
                RowHeight = 50,
                ItemTemplate = new DataTemplate(typeof(CustomViewCell)),
                ItemsSource = CustomPickerViewModel.CustomPickerItems,
                //HeightRequest =(CustomPickerViewModel.CustomPickerItems.Count) * 50
            };
            listView.ItemSelected += OnListViewItemSelected;

            Button cancelButton = new Button
            {
                Text = "Cancel",
                TextColor = Color.FromHex("#8B00FF"),
                HorizontalOptions = LayoutOptions.End,
                //Padding = new Thickness(0, 0, 0, 0)
            };
            cancelButton.Clicked += OnCancel;

            listStackLayout.Children.Add(listView);
            listStackLayout.Children.Add(cancelButton);

            popupFrame = new Frame
            {
                HasShadow = true,
                IsVisible = false,
                Scale = 0,
                BackgroundColor = Color.White,
                Content = listStackLayout
            };

            AbsoluteLayout.SetLayoutFlags(popupFrame, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(popupFrame,
                new Rectangle(0.5, 0.5, 300, (CustomPickerViewModel.CustomPickerItems.Count + 2) * 50));

            Button popupButton = new Button
            {
                FontSize = 20
            };
            popupButton.Clicked += Popup_Button_Clicked;
            popupButton.SetBinding(Button.TextProperty, "Selected");

            StackLayout innerContentsStackLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = {popupButton}
            };

            outerLayout.Children.Add(innerContentsStackLayout);
            outerLayout.Children.Add(popupFrame);

            BindingContext = new MainPageItem();

            Content = outerLayout;
        }

        private void Popup_Button_Clicked(object sender, EventArgs e)
        {
            if (!popupFrame.IsVisible)
            {
                BackgroundColor = Color.FromHex("#6f6f6f");
                popupFrame.IsVisible = !popupFrame.IsVisible;
                popupFrame.AnchorX = 0.5;
                popupFrame.AnchorY = 0.5;

                Animation scaleAnimation = new Animation(
                    f => popupFrame.Scale = f,
                    0.5,
                    1,
                    Easing.SinInOut);

                Animation fadeAnimation = new Animation(
                    f => popupFrame.Opacity = f,
                    0.5,
                    1,
                    Easing.SinInOut);

                scaleAnimation.Commit(popupFrame, "popupScaleAnimation", 100);
                fadeAnimation.Commit(popupFrame, "popupFadeAnimation", 100);
            }
            else
            {
                PopupFadeAway();
            }
        }

        private void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem != null)
            {
                ChangeTextToSelectedItem(((CustomPickerItems) args.SelectedItem).name);
                PopupFadeAway();
            }
        }

        private void ChangeTextToSelectedItem(string text)
        {
            if (BindingContext is MainPageItem)
            {
                ((MainPageItem) BindingContext).Selected = text;
                Console.WriteLine("@@@@@@" + text);
            }
        }

        private void OnCancel(object sender, EventArgs e)
        {
            PopupFadeAway();
        }

        private async void PopupFadeAway()
        {
            BackgroundColor = Color.White;
            await Task.WhenAny<bool>
            (
                popupFrame.FadeTo(0, 200, Easing.SinInOut)
            );

            popupFrame.IsVisible = !popupFrame.IsVisible;
        }
    }

    public class CustomViewCell : ViewCell
    {
        public CustomViewCell()
        {
            StackLayout layout = new StackLayout() {Padding = new Thickness(2, 15)};
            layout.Orientation = StackOrientation.Horizontal;
            Label nameLabel = new Label() {HorizontalOptions = LayoutOptions.CenterAndExpand};
            nameLabel.SetBinding(Label.TextProperty, "name");
            nameLabel.SetBinding(Label.TextColorProperty, "color");
            layout.Children.Add(nameLabel);

            View = layout;
        }
    }
}