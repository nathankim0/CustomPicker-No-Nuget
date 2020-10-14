using System;
using Xamarin.Forms;

namespace CustomPickMePage
{
    public class MainPage2 : ContentPage
    {
        public MainPage2()
        {
            AbsoluteLayout outerLayout = new AbsoluteLayout
            {
                Padding = new Thickness(50)
            };

            Button popupButton = new Button
            {
                FontSize = 30,
                Text="Category Select"
            };
            popupButton.Clicked += Popup_Button_Clicked;
            
            MessagingCenter.Subscribe<object, string>(this, "Hi", (sender, arg) =>
            {
                popupButton.Text = arg;
            });

            StackLayout innerContentsStackLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = {popupButton}
            };
            outerLayout.Children.Add(innerContentsStackLayout);

            Content = outerLayout;
        }

        private async void Popup_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new PopupPage());
        }
    }
}