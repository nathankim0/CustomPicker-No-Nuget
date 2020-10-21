using System;
using Xamarin.Forms;

namespace CustomPickMePage
{
    public class MainPage2 : ContentPage
    {
        public MainPage2()
        {
            Image image = new Image();
            Button popupButton = new Button
            {
                FontSize = 40,
                Text="Category Select"
            };
            popupButton.Clicked += async (sender, e)=>
            {
                await Navigation.PushModalAsync(new MainPage2_PickerPage());

            };
            
            // Subscribe a Message (From MainPage2_PickerPage)
            MessagingCenter.Subscribe<MainPage2_PickerPage, string>(this, "text", (sender, arg) =>
            {
                popupButton.Text = arg;
            });

            MessagingCenter.Subscribe<MainPage2_PickerPage, string>(this, "source", (sender, arg) =>
            {
                image.Source = arg;
            });
            
            Content = new StackLayout
            {
                Children =
                {
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                      Children = {image, popupButton},
                      HorizontalOptions=LayoutOptions.Center,
                      VerticalOptions=LayoutOptions.CenterAndExpand
                     }
                }
            };
        }
    }
}