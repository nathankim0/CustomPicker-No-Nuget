using System;
using Xamarin.Forms;
using Xamarin.Forms.Markup;

namespace CustomPickMe
{
    public class MainPage : ContentPage
    {
        public MainPage()
        {
            Button cancelButton = button("Cancel", Color.FromHex("#8B00FF"));
            cancelButton.Clicked += OnCancel;


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
            listView.HeightRequest = (CustomPickerViewModel.CustomPickerItems.Count) * 50;

            listView.ItemSelected += OnListViewItemSelected;

            listStackLayout.Children.Add(listView);
            listStackLayout.Children.Add(buttonStackLayout);

            outerStackLayout.Children.Add(listStackLayout);

            Frame popupFrame = new Frame
            {
                HashShadow = true,


            };

            /*
                    < Frame
            x: Name = "popuplayout"
            HasShadow = "True"
            IsVisible = "False"
            Scale = "0"
            BackgroundColor = "White"
            AbsoluteLayout.LayoutFlags = "All"
            AbsoluteLayout.LayoutBounds = "1,1,0.5,0.5" >
            < StackLayout >
                < Label Text = "One" />
 
                 < Label Text = "Two" />
  
                  < Label Text = "Three" />
   
                   < Label Text = "Four" />
    
                    < Label Text = "Five" />
     
                     < Label Text = "Six" />
      
                  </ StackLayout >
      
              </ Frame >
            */


            Label label = new Label();
            label.SetBinding(Label.TextProperty, "Selected");
            label.FontSize = 15;

            this.BindingContext = new MainPageItems();
        }
        private Button button(String text, Color color)
        {
            return new Button
            {
                Text = text,
                TextColor = color,
                Padding = new Thickness(0, 0, 20, 10)
            };
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (!this.popuplayout.IsVisible)
            {
                this.popuplayout.IsVisible = !this.popuplayout.IsVisible;
                this.popuplayout.AnchorX = 1;
                this.popuplayout.AnchorY = 1;

                Animation scaleAnimation = new Animation(
                    f => this.popuplayout.Scale = f,
                    0.5,
                    1,
                    Easing.SinInOut);

                Animation fadeAnimation = new Animation(
                    f => this.popuplayout.Opacity = f,
                    0.2,
                    1,
                    Easing.SinInOut);

                scaleAnimation.Commit(this.popuplayout, "popupScaleAnimation", 250);
                fadeAnimation.Commit(this.popuplayout, "popupFadeAnimation", 250);
            }
            else
            {
                await Task.WhenAny<bool>
                  (
                    this.popuplayout.FadeTo(0, 200, Easing.SinInOut)
                  );

                this.popuplayout.IsVisible = !this.popuplayout.IsVisible;
            }
        }
        private void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            ((ListView)sender).SelectedItem = null;

            if (args.SelectedItem != null)
            {
                ChangeTextToSelectedItem(((CustomPickerItems)args.SelectedItem).name);
            }
        }

        private async void ChangeTextToSelectedItem(string text)
        {
            if (BindingContext is MainPageItems)
            {
                ((MainPageItems)BindingContext).Selected = text;
                Console.WriteLine("@@@@@@" + text);
            }
            await PopupNavigation.Instance.PopAsync();
        }

        private async void OnCancel(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
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