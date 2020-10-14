using System;
using CustomPickMe;
using Xamarin.Forms;

namespace CustomPicker2
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
                FontSize = 20
            };
            popupButton.Clicked += Popup_Button_Clicked;
            popupButton.SetBinding(Button.TextProperty, "Selected2");
            
            StackLayout innerContentsStackLayout=new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { popupButton}
            };
            outerLayout.Children.Add(innerContentsStackLayout);

            BindingContext = new MainPageItem2();
            
            Content = outerLayout;
        }
        
        private async void Popup_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new PopupPage());
        }
    }
}