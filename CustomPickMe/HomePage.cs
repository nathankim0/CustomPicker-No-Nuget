using System;
using CustomPickMeMain;
using CustomPickMePage;
using Xamarin.Forms;

namespace CustomPickMe.View
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            Button PopupPicker = new Button
            {
                Text = "MainPage1",
                FontSize = 30
            };
            PopupPicker.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new MainPage1());
            };

            Button PagePicker = new Button
            {
                Text = "MainPage2",
                FontSize = 30,
                Margin=30
            };
            PagePicker.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new MainPage2());
            };

            Content = new StackLayout
            {
                Padding = 40,
                Children =
                {
                    new StackLayout
                    {
                        VerticalOptions=LayoutOptions.CenterAndExpand,
                        HorizontalOptions=LayoutOptions.CenterAndExpand,
                        Children =
                        {
                            PopupPicker,
                            PagePicker
                        }
                    }
                    
                }
            };
        }
    }
}