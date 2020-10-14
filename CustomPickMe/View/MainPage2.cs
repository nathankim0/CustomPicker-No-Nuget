using System;
using Xamarin.Forms;

namespace CustomPickMePage
{
    public class MainPage2 : ContentPage
    {
        private static string _selected2 = "Category(PopupPage)";

        public static string Selected2
        {
            get { return _selected2; }
            set
            {
                _selected2 = value;
            }
        }
        public MainPage2()
        {
            AbsoluteLayout outerLayout = new AbsoluteLayout
            {
                Padding = new Thickness(50)
            };

            Button popupButton = new Button
            {
                FontSize = 20,
                Text = Selected2
            };
            popupButton.Clicked += Popup_Button_Clicked;
            
            MessagingCenter.Subscribe<object, string>(this, "Hi", async (sender, arg) =>
            {
                popupButton.Text = arg;
                //await DisplayAlert("Message received", "arg=" + arg, "OK");
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