using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using CustomPicker;

namespace CustomPickMe
{
    public class MainPage : ContentPage
    {
        private Frame popupFrame;
        public MainPage()
        {
            for(int i=0;i<5;i++)
            {
                CustomPickerViewModel.CustomPickerItems.Add(new CustomPickerItems("Added at mainpage",Color.Chocolate));
            }
            AbsoluteLayout outerLayout = new AbsoluteLayout
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
            AbsoluteLayout.SetLayoutBounds(popupFrame, new Rectangle(0.5,0.5,300,(CustomPickerViewModel.CustomPickerItems.Count+2) * 50));
            
            Button popupButton = new Button
            {
                Text = "popup1",
                FontSize = 20
                //TextColor = Color.FromHex("#8B00FF"),
                //Padding = new Thickness(0, 0, 20, 10)
            };
            popupButton.Clicked += Popup_Button_Clicked;

            Label label = new Label
            {
                FontSize = 20,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center
            };
            label.SetBinding(Label.TextProperty, "Selected");
            
            StackLayout innerContentsStackLayout=new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { popupButton, label}
            };

            outerLayout.Children.Add(innerContentsStackLayout);
            outerLayout.Children.Add(popupFrame);


            this.BindingContext = new MainPageItem();
            
            Content = outerLayout;
        }
        
        private async void Popup_Button_Clicked(object sender, EventArgs e)
        {
            if (!this.popupFrame.IsVisible)
            {
                BackgroundColor=Color.FromHex("#6f6f6f");
                this.popupFrame.IsVisible = !this.popupFrame.IsVisible;
               // this.popupFrame.AnchorX = 1;
               // this.popupFrame.AnchorY = 1;

                Animation scaleAnimation = new Animation(
                    f => this.popupFrame.Scale = f,
                    1,
                    1,
                    Easing.SinInOut);

                Animation fadeAnimation = new Animation(
                    f => this.popupFrame.Opacity = f,
                    1,
                    1,
                    Easing.SinInOut);

                scaleAnimation.Commit(this.popupFrame, "popupScaleAnimation", 500);
                fadeAnimation.Commit(this.popupFrame, "popupFadeAnimation", 500);
            }
            else
            {
                PopupFadeAway();
            }
        }
        private void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            ((ListView)sender).SelectedItem = null;

            if (args.SelectedItem != null)
            {
                ChangeTextToSelectedItem(((CustomPickerItems)args.SelectedItem).name);
                PopupFadeAway();
            }
        }

        private void ChangeTextToSelectedItem(string text)
        {
            if (BindingContext is MainPageItem)
            {
                ((MainPageItem)BindingContext).Selected = text;
                Console.WriteLine("@@@@@@" + text);
            }
        }

        private async void OnCancel(object sender, EventArgs e)
        {
            PopupFadeAway();
        }
        
        private async void PopupFadeAway()
        {
            BackgroundColor=Color.White;
            await Task.WhenAny<bool>
            (
                this.popupFrame.FadeTo(0, 200, Easing.SinInOut)
            );

            this.popupFrame.IsVisible = !this.popupFrame.IsVisible;
        }
    }
    public class CustomViewCell : ViewCell
    {
        public CustomViewCell()
        {
            StackLayout layout = new StackLayout() { Padding = new Thickness(2, 15) };
            layout.Orientation = StackOrientation.Horizontal;
            Label nameLabel = new Label() { HorizontalOptions = LayoutOptions.CenterAndExpand };
            nameLabel.SetBinding(Label.TextProperty, "name");
            nameLabel.SetBinding(Label.TextColorProperty, "color");
            layout.Children.Add(nameLabel);

            View = layout;
        }
    }
}