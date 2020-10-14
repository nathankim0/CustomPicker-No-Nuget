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
                Text = "Popup Picker",
                FontSize = 30
            };
            PopupPicker.Clicked += PopupPickerClick;

            Button PagePicker = new Button
            {
                Text = "Page Picker",
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
            await Navigation.PushAsync(new MainPage());
        }

        private async void PagePickerClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage2());
        }

        
    }
}