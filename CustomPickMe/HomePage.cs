using System;
using CustomPickMe.ViewFolder;
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
            PopupPicker.Clicked += async (sender, e) => { await Navigation.PushAsync(new MainPage1()); };

            Button PagePicker = new Button
            {
                Text = "Page Picker",
                FontSize = 30,
                Margin = 30
            };
            PagePicker.Clicked += async (sender, e) => { await Navigation.PushAsync(new MainPage2()); };
            Button MyCustomLayout = new Button
            {
                Text = "CustomLayout Page",
                FontSize = 30,
                Margin = 30
            };
            MyCustomLayout.Clicked += async (sender, e) => { await Navigation.PushAsync(new MainPage3()); };
            Button MainPage4 = new Button
            {
                Text = "Bottom Sheet",
                FontSize = 30,
                Margin = 30
            };

            MainPage4.Clicked += async (sender, e) => { await Navigation.PushAsync(new MainPage4()); };
            Button MyPage = new Button
            {
                Text = "Bottom Sheet2",
                FontSize = 30,
                Margin = 30
            };

            MyPage.Clicked += async (sender, e) => { await Navigation.PushAsync(new MyPage()); };

            StackLayout stack = new StackLayout
            {
                Padding = 40,
                Children =
                {
                    new StackLayout
                    {
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        Children =
                        {
                            PopupPicker,
                            PagePicker
                        }
                    }
                }
            };
            var scroll = new ScrollView
            {
                Content = stack
            };

            Content = scroll;
        }
    }
}