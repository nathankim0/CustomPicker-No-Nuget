using System;
using Xamarin.Forms;

namespace CustomPickMePage
{
    public class MainPage2 : ContentPage
    {
        public MainPage2()
        {
            StackLayout layout = new StackLayout();
            Image image = new Image
            {
            };

            Button popupButton = new Button
            {
                FontSize = 40,
                Text="Category Select"
            };
            popupButton.Clicked += Popup_Button_Clicked;
            
            MessagingCenter.Subscribe<object, string>(this, "text", (sender, arg) =>
            {
                popupButton.Text = arg;
            });
            MessagingCenter.Subscribe<object, string>(this, "source", (sender, arg) =>
            {
                image.Source = arg;
            });

            StackLayout innerContentsStackLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = {image, popupButton},
                HorizontalOptions=LayoutOptions.Center,
                VerticalOptions=LayoutOptions.CenterAndExpand
            };
            layout.Children.Add(innerContentsStackLayout);
            Content = layout;
        }

        private async void Popup_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new PopupPage());
        }
    }
}