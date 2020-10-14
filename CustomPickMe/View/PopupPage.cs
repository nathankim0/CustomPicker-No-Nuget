using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using CustomPicker;

namespace CustomPicker2
{
    public class PopupPage :ContentPage
    {
        public PopupPage()
        {
            StackLayout listStackLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.White
            };

            ListView listView = new ListView
            {
                Footer = "",
                RowHeight = 50,
                ItemTemplate = new DataTemplate(typeof(CustomViewCellPopupPage)),
                ItemsSource = CustomPickerViewModel.CustomPickerItems,
                HeightRequest =(CustomPickerViewModel.CustomPickerItems.Count) * 50
            };
            listView.ItemSelected += OnListViewItemSelected;

            Button cancelButton = new Button
            {
                Text = "Cancel",
                TextColor = Color.FromHex("#8B00FF"),
                HorizontalOptions = LayoutOptions.End,
                Padding=20
            };
            cancelButton.Clicked += OnCancel;
            
            listStackLayout.Children.Add(listView);
            listStackLayout.Children.Add(cancelButton);

            Content = listStackLayout;
         }
        private async void OnCancel(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        private async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem != null)
            {
                ChangeTextToSelectedItem(((CustomPickerItems)args.SelectedItem).name);
            }
            await Navigation.PopModalAsync();
        }

        private void ChangeTextToSelectedItem(string text)
        {
            if (BindingContext is MainPageItem2)
            {
                ((MainPageItem2)BindingContext).Selected2 = text;
                Console.WriteLine("@@@@@@" + text);
            }
        }
    public class CustomViewCellPopupPage : ViewCell
    {
        public CustomViewCellPopupPage()
        {
            StackLayout layout = new StackLayout() { Padding = new Thickness(2, 15) };
            layout.Orientation = StackOrientation.Horizontal;
            Label nameLabel = new Label() { HorizontalOptions = LayoutOptions.CenterAndExpand };
            nameLabel.SetBinding(Label.TextProperty, "name");
            nameLabel.SetBinding(Label.TextColorProperty, "color");
            layout.Children.Add(nameLabel);

            View = layout;
        }
    }
    }
}