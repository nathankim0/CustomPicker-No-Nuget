using System;
using Xamarin.Forms;
using CustomPicker;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace CustomPickMePage
{
    public class MainPage2_PopupPage : ContentPage
    {
        /* MainPage2_PopupPage 시작 */
        public MainPage2_PopupPage()
        {
            // ios modal 스타일 설정
            On<iOS>().SetModalPresentationStyle(UIModalPresentationStyle.Automatic);

            // 상단 왼쪽 취소 버튼
            var cancelButton = new Button
            {
                Text = "Cancel",
                BackgroundColor = Color.FromHex("#EEEEEE"),
                TextColor = Color.FromHex("#8B00FF"),
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(20, 0, 0, 0)
            };
            cancelButton.Clicked += OnCancel;

            // 상단 가운데 타이틀
            var titleLabel = new Label
            {
                Text = "Select Category",
                TextColor = Color.Black,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = 16
            };

            // 상단 타이틀바 Grid (3X1)
            var grid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition()
                },
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            grid.Children.Add(cancelButton, 0, 0);
            grid.Children.Add(titleLabel, 1, 0);


            // 타이틀바 + CollectionView(Picker)가 들어갈 stacklayout 정의
            var modalLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    // 배경색상
                    new StackLayout
                    {
                        BackgroundColor = Color.FromHex("#EEEEEE"),
                        Orientation = StackOrientation.Vertical,
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        HeightRequest = 50,
                        MinimumHeightRequest = 50,
                        Children =
                        {
                            grid // 타이틀바 추가
                        }
                    }
                }
            };

            /* collectionView 시작 */
            var collectionView = new CollectionView   // Picker가 될 Collection View
            {
            ItemsSource = CustomPickerViewModel.CustomPickerItems, // 목록 항목들
                VerticalOptions = LayoutOptions.FillAndExpand, // 아래 빈공간 없앰

                /* ItemTemplate 시작 */
                ItemTemplate = new DataTemplate(() =>
                {
                    // 목록들이 들어갈 Grid
                    Grid itemGrid = new Grid
                    {
                        //Padding = 15,
                        RowDefinitions =
                        {
                            new RowDefinition
                            {
                                Height=new GridLength(50)
                            }
                        },
                        ColumnDefinitions =
                        {
                            new ColumnDefinition(),
                            new ColumnDefinition(),
                            new ColumnDefinition()
                        }
                    };

                    // 목록 아이콘
                    Image image = new Image();
                    image.SetBinding(Image.SourceProperty, "imagesource");
                    itemGrid.Children.Add(image, 0, 0);

                    // 목록 이름
                    Label nameLabel = new Label()
                    {
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand
                    };
                    // 목록 이름과 색상을 바인딩
                    nameLabel.SetBinding(Label.TextProperty, "name");
                    nameLabel.SetBinding(Label.TextColorProperty, "color");
                    itemGrid.Children.Add(nameLabel, 1, 0); // Grid (1,0)부분에 목록 라벨 추가

                    // 목록간 구분선 정의
                    var separator = new BoxView
                    {
                        HeightRequest = 1,
                        BackgroundColor = Color.Black,
                        VerticalOptions = LayoutOptions.End
                    };
                    itemGrid.Children.Add(separator);
                    Grid.SetColumnSpan(separator, 3); // ColumnSpan을 통해 일자로 쭉

                    // 목록 클릭 이벤트 설정 (TapGestureRecognizer)
                    var tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += (s, e) =>
                    {
                        OnCollcetionViewItemSelected(nameLabel.Text, (FileImageSource)image.Source); // 목록 이름과 아이콘
                    };
                    itemGrid.GestureRecognizers.Add(tapGestureRecognizer); // Grid에 만든 탭제스처 추가

                    return itemGrid;
                })
                /* ItemTemplate 끝 */
            };
            /* collectionView 끝 */

            modalLayout.Children.Add(collectionView); // 하단 목록 추가

            Content = modalLayout;
        }
        /* MainPage2_PopupPage 끝 */


        // Picker 취소 버튼 함수
        private async void OnCancel(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        // 목록 선택 함수
        private async void OnCollcetionViewItemSelected(string text, string source)
        {
            if (text != null && text != "")
            {
                // Messaging Center를 통해 Publish a Message.
                MessagingCenter.Send(this, "text", text); // 목록 이름
                MessagingCenter.Send(this, "source", source); // 목록 아이콘

                await Navigation.PopModalAsync();
            }
        }
    }
}



        /*
        var listView = new Xamarin.Forms.ListView
        {
            Footer = "",
            RowHeight = 50,
            ItemTemplate = new DataTemplate(typeof(CustomViewCellPopupPage)),
            ItemsSource = CustomPickerViewModel.CustomPickerItems,
            HeightRequest = (CustomPickerViewModel.CustomPickerItems.Count) * 50
        };
        listView.ItemSelected += OnListViewItemSelected;
        
        //modalLayout.Children.Add(listView);
        
        private async void OnListViewItemSelected(object sender, SelectionChangedEventArgs args)
        {
            if (args.CurrentSelection != null)
            {
                MessagingCenter.Send<object, string>(this, "Hi", ((CustomPickerItems) args.CurrentSelection).name);

                await Navigation.PopModalAsync();
            }
           
        }

        public class CustomViewCellPopupPage : ViewCell
        {
            public CustomViewCellPopupPage()
            {
                StackLayout layout = new StackLayout() {Padding = new Thickness(2, 15)};
                layout.Orientation = StackOrientation.Horizontal;
                Label nameLabel = new Label() {HorizontalOptions = LayoutOptions.CenterAndExpand};
                nameLabel.SetBinding(Label.TextProperty, "name");
                nameLabel.SetBinding(Label.TextColorProperty, "color");
                layout.Children.Add(nameLabel);

                View = layout;
            }
        }
        */
    
