using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using CustomPicker;

namespace CustomPickMeMain
{
    public class MainPage1 : ContentPage
    {
        private readonly Frame _popupFrame;
        private readonly StackLayout _innerContentsStackLayout;

        public MainPage1()
        {
            var outerLayout = new AbsoluteLayout();

            var listStackLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.White
            };

            var listView = new ListView
            {
                Footer = "",
                RowHeight = 50,
                ItemTemplate = new DataTemplate(typeof(CustomViewCell)),
                ItemsSource = CustomPickerViewModel.CustomPickerItems
            };
            listView.ItemSelected += OnListViewItemSelected;

            var cancelButton = new Button
            {
                Text = "Cancel",
                TextColor = Color.FromHex("#8B00FF"),
                HorizontalOptions = LayoutOptions.End,
                BackgroundColor = Color.White
            };
            cancelButton.Clicked += OnCancel;

            listStackLayout.Children.Add(listView);
            listStackLayout.Children.Add(cancelButton);

            _popupFrame = new Frame
            {
                HasShadow = true,
                IsVisible = false,
                Scale = 0,
                BackgroundColor = Color.White,
                Content = listStackLayout
            };

            AbsoluteLayout.SetLayoutFlags(_popupFrame, AbsoluteLayoutFlags.PositionProportional);


            AbsoluteLayout.SetLayoutBounds(_popupFrame,
                CustomPickerViewModel.CustomPickerItems.Count <= 8
                    ? new Rectangle(0.5, 0.5, 300, (CustomPickerViewModel.CustomPickerItems.Count + 2) * 50)
                    : new Rectangle(0.5, 0.5, 300, 550));

            var popupButton = new Button
            {
                FontSize = 30
            };
            popupButton.Clicked += Popup_Button_Clicked;
            popupButton.SetBinding(Button.TextProperty, "Selected");

            _innerContentsStackLayout = new StackLayout
            {
                BackgroundColor = Color.Bisque,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = {popupButton}
            };
            
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => { PopupFadeAway(); };
            _innerContentsStackLayout.GestureRecognizers.Add(tapGestureRecognizer);

            outerLayout.Children.Add(_innerContentsStackLayout, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
            outerLayout.Children.Add(_popupFrame);

            BindingContext = new MainPageItem();

            Content = outerLayout;
        }

        private void Popup_Button_Clicked(object sender, EventArgs e)
        {
            if (_popupFrame.IsVisible == false)
            {
                _innerContentsStackLayout.BackgroundColor = Color.FromHex("#6f6f6f");
                _popupFrame.IsVisible = true;


                popupFrame.AnchorX = 0.5;
                popupFrame.AnchorY = 0.5;


                Animation scaleAnimation = new Animation(
                    f => _popupFrame.Scale = f,
                    0.95,
                    1,
                    Easing.CubicOut);

                Animation fadeAnimation = new Animation(
                    f => _popupFrame.Opacity = f,
                    0.95,
                    1,
                    Easing.CubicOut);

                scaleAnimation.Commit(_popupFrame, "popupScaleAnimation", 100);
                fadeAnimation.Commit(_popupFrame, "popupFadeAnimation", 100);
            }
            else
            {
                PopupFadeAway();
            }
        }

        private void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem == null) return;
            ChangeTextToSelectedItem(((CustomPickerItems) args.SelectedItem).name);
            PopupFadeAway();
        }

        private void ChangeTextToSelectedItem(string text)
        {
            if (!(BindingContext is MainPageItem)) return;
            ((MainPageItem) BindingContext).Selected = text;
            Console.WriteLine("@@@@@@" + text);
        }

        private void OnCancel(object sender, EventArgs e)
        {
            PopupFadeAway();
        }

        private async void PopupFadeAway()
        {
            if (_popupFrame.IsVisible != true) return;
            _innerContentsStackLayout.BackgroundColor = Color.White;
            
                await Task.WhenAny<bool>
                (
                    _popupFrame.FadeTo(0, 200, Easing.SinOut)
                );
                
            _popupFrame.IsVisible = false;
        }
    }

    public class CustomViewCell : ViewCell
    {
        public CustomViewCell()
        {
            var layout = new StackLayout {Padding = new Thickness(2, 15), Orientation = StackOrientation.Horizontal};
            var nameLabel = new Label() {HorizontalOptions = LayoutOptions.CenterAndExpand};
            nameLabel.SetBinding(Label.TextProperty, "name");
            nameLabel.SetBinding(Label.TextColorProperty, "color");
            layout.Children.Add(nameLabel);

            View = layout;
        }
    }
}