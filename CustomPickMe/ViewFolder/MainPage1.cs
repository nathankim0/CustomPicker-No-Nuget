using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using CustomPicker;

namespace CustomPickMeMain
{
    public class MainPage1 : ContentPage
    {
        private Frame popupFrame;
        private AbsoluteLayout outerLayout;
        private StackLayout innerContentsStackLayout;

        public MainPage1()
        {
            outerLayout = new AbsoluteLayout();

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
                ItemsSource = CustomPickerViewModel.CustomPickerItems
            };
            listView.ItemSelected += OnListViewItemSelected;

            Button cancelButton = new Button
            {
                Text = "Cancel",
                TextColor = Color.FromHex("#8B00FF"),
                HorizontalOptions = LayoutOptions.End,
                BackgroundColor = Color.White
                //Padding = new Thickness(0, 0, 0, 0)
            };
            cancelButton.Clicked += OnCancel;

            listStackLayout.Children.Add(listView);
            listStackLayout.Children.Add(cancelButton);

            popupFrame = new Frame
            {
                HasShadow = true,
                IsVisible = false,
                //Scale = 0,
                BackgroundColor = Color.White,
                Content = listStackLayout
            };

            AbsoluteLayout.SetLayoutFlags(popupFrame, AbsoluteLayoutFlags.PositionProportional);


            if (CustomPickerViewModel.CustomPickerItems.Count <= 8)
            {
                AbsoluteLayout.SetLayoutBounds(popupFrame,
               new Rectangle(0.5, 0.5, 300, (CustomPickerViewModel.CustomPickerItems.Count + 2) * 50));
            }
            else
            {
                AbsoluteLayout.SetLayoutBounds(popupFrame,
                    new Rectangle(0.5, 0.5, 300, 550));
            }

            Button popupButton = new Button
            {
                FontSize = 30
            };
            popupButton.Clicked += Popup_Button_Clicked;
            popupButton.SetBinding(Button.TextProperty, "Selected");

            innerContentsStackLayout = new StackLayout
            {
                BackgroundColor=Color.Bisque,
                VerticalOptions=LayoutOptions.FillAndExpand,
                HorizontalOptions=LayoutOptions.FillAndExpand,
               // Orientation = StackOrientation.Vertical,
                Children = {popupButton }
            };

            /*
            Grid textGrid = new Grid();
            for(int i = 0; i < 10; i++)
            {
                textGrid.RowDefinitions.Add(new RowDefinition());
                textGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    textGrid.Children.Add(new Label { Text = "가려짐", FontSize = 15 },i,j);
                }
            }
            innerContentsStackLayout.Children.Add(textGrid);
            */

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                PopupFadeAway();
            };
            innerContentsStackLayout.GestureRecognizers.Add(tapGestureRecognizer);

            outerLayout.Children.Add(innerContentsStackLayout, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
            outerLayout.Children.Add(popupFrame);

            BindingContext = new MainPageItem();

            Content = outerLayout;
        }

        private void Popup_Button_Clicked(object sender, EventArgs e)
        {
            if (popupFrame.IsVisible == false)
            {
                innerContentsStackLayout.BackgroundColor = Color.FromHex("#6f6f6f");
                popupFrame.IsVisible = true;


                //popupFrame.AnchorX = 0.5;
                //popupFrame.AnchorY = 0.5;
                /*

                Animation scaleAnimation = new Animation(
                    f => popupFrame.Scale = f,
                    0.95,
                    1,
                    Easing.SinInOut);

                Animation fadeAnimation = new Animation(
                    f => popupFrame.Opacity = f,
                    0.95,
                    1,
                    Easing.SinInOut);

                scaleAnimation.Commit(popupFrame, "popupScaleAnimation", 100);
                fadeAnimation.Commit(popupFrame, "popupFadeAnimation", 100);
                */
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
            if (popupFrame.IsVisible==true)
            {
                innerContentsStackLayout.BackgroundColor = Color.White;
                /*
                await Task.WhenAny<bool>
                (
                     popupFrame.FadeTo(0, 200, Easing.SinOut)
                );
                */
                popupFrame.IsVisible = false;
            }
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