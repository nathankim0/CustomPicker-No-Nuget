using System;
using BottomSheet;
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
                Text = "PopupPicker",
                FontSize = 30
            };
            PopupPicker.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new MainPage1());
            };

            Button PagePicker = new Button
            {
                Text = "PagePicker",
                FontSize = 30,
                Margin=30
            };
            PagePicker.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new MainPage2());
            };
            Button MyCustomLayout = new Button
            {
                Text = "MyPage",
                FontSize = 30,
                Margin=30
            };
            MyCustomLayout.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new MainPage3());
            };
            Button bottomDrawer = new Button
            {
                Text = "bottomDrawer",
                FontSize = 30,
                Margin = 30
            };
            bottomDrawer.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new BottomDrawer());
            };
            Button MybottomSheet = new Button
            {
                Text = "MybottomSheet",
                FontSize = 30,
                Margin = 30
            };

            MybottomSheet.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new MainPage4());
            };
            Button MainPage = new Button
            {
                Text = "MainPage",
                FontSize = 30,
                Margin = 30
            };

            MainPage.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new MainPage());
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
                            PagePicker,
                            MyCustomLayout,
                            bottomDrawer,
                            MybottomSheet,
                            MainPage

                        }
                    }
                    
                }
            };
        }
    }
}