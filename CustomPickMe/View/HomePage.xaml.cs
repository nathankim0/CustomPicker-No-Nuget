using System;
using CustomPicker2;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CustomPickMe.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }
        private async void PopupPage_Button_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage2());
        }
        private async void PopupModal_Button_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}