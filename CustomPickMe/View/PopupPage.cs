using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using CustomPicker;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace CustomPicker2
{
    public class PopupPage : ContentPage
    {
        public PopupPage()
        {
            On<iOS>().SetModalPresentationStyle(UIModalPresentationStyle.Automatic);

            Title = "Category Select";

            var cancelButton = new Button
            {
                Text = "Cancel",
                TextColor = Color.FromHex("#8B00FF"),
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(20,0,0,0)
            };
            cancelButton.Clicked += OnCancel;

            var modalLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    new StackLayout
                    {
                        BackgroundColor = Color.FromHex("#EEEEEE"),
                        Orientation = StackOrientation.Vertical,
                        VerticalOptions = LayoutOptions.StartAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        HeightRequest=50,
                        MinimumHeightRequest=50,
                        Children =
                        {
                            new StackLayout
                            {
                                Orientation = StackOrientation.Horizontal,
                                VerticalOptions = LayoutOptions.FillAndExpand,
                              //Padding = new Thickness(3, 0, 0, 0),
                                Children =
                                {
                                    cancelButton,
                                    new Label
                                    {
                                        Text="Select Category",
                                        TextColor=Color.Black,
                                        HorizontalOptions=LayoutOptions.CenterAndExpand,
                                        VerticalOptions=LayoutOptions.FillAndExpand,
                                        VerticalTextAlignment=TextAlignment.Center,
                                        HorizontalTextAlignment=TextAlignment.Center,
                                        FontSize=16
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var listView = new Xamarin.Forms.ListView
            {
                Footer = "",
                RowHeight = 50,
                ItemTemplate = new DataTemplate(typeof(CustomViewCellPopupPage)),
                ItemsSource = CustomPickerViewModel.CustomPickerItems,
                HeightRequest = (CustomPickerViewModel.CustomPickerItems.Count) * 50
            };
            listView.ItemSelected += OnListViewItemSelected;

            modalLayout.Children.Add(listView);

            Content = modalLayout;
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