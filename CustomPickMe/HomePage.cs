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
            PopupPicker.Clicked += PopupPickerClick;

            Button PagePicker = new Button
            {
                Text = "MainPage2",
                FontSize = 30
            };
            PagePicker.Clicked += PagePickerClick;

            var layout = new StackLayout
            {
                Padding = 40,
                Children =
                {
                    PopupPicker,
                    PagePicker
                }
            };
            Content = layout;
        }
        private async void PopupPickerClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage1());
        }

        private async void PagePickerClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage2());
        }

        
    }
}