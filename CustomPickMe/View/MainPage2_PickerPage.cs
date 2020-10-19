using System;
using Xamarin.Forms;
using CustomPicker;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using System.Collections.Generic;
using System.Linq;
/// <summary>
/// 
/// 20201016_Nathan
/// PickerPage
/// MainPage2 에서 넘어옴.
/// *iOS, Andorid 모양이 다름.
/// 
/// </summary>
///

namespace CustomPickMePage
{
    public class MainPage2_PickerPage : ContentPage
    {
        private readonly Button _button;
        /* MainPage2_PopupPage 시작 */
        public MainPage2_PickerPage()
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
                EmptyView="없네요 ㅎ", //데이터 없을때 띄우는거
                Margin = new Thickness(0, 0, 0, 20), // 아이폰 네비게이션바 안겹치게 
                ItemsSource = CustomPickerViewModel.CustomPickerItems, // 목록 항목들
                VerticalOptions = LayoutOptions.FillAndExpand, // 아래 빈공간 없앰
                ItemsLayout = new GridItemsLayout(2, ItemsLayoutOrientation.Vertical),
               // {
              //      VerticalItemSpacing = 20,
               //     HorizontalItemSpacing = 30
              //  },
               // ItemSizingStrategy = ItemSizingStrategy.MeasureAllItems,

                /* ItemTemplate 시작 */
                ItemTemplate = new DataTemplate(() =>
                {
                    // 목록들이 들어갈 Grid (한 목록당 3행 2열)
                    Grid itemGrid = new Grid
                    {
                        //Padding = 15,
                        RowDefinitions =
                        {
                            new RowDefinition // 첫 번째 줄
                            {
                                Height=GridLength.Auto
                            },
                            new RowDefinition // 두 번째 줄
                            {
                                Height=GridLength.Auto
                            }
                        },
                        ColumnDefinitions =
                        {
                            // 3행
                            new ColumnDefinition(),
                            new ColumnDefinition(),
                            new ColumnDefinition()
                        }
                    };

                    // 목록 아이콘
                    Image image = new Image
                    {
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        Aspect=Aspect.AspectFit,
                        HeightRequest=52,
                        WidthRequest=52
                    };
                    image.SetBinding(Image.SourceProperty, "imagesource");
                    itemGrid.Children.Add(image, 0, 0);

                    /*
                    // 아이콘 설명 라벨 배경
                    BoxView labelBox = new BoxView
                    {
                        BackgroundColor = Color.White,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                    };
                    Grid.SetRow(labelBox, 1); // 1열, 아이콘 아래 
                    itemGrid.Children.Add(labelBox);
                    */

                    // 아이콘 설명 라벨
                    Label imageLabel = new Label()
                    {
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Start,
                        //TextColor = Color.Black,
                        FontSize = 12
                    };
                    imageLabel.SetBinding(Label.TextProperty, "imagesource");
                    Grid.SetRow(imageLabel, 1); // 1열, 아이콘 아래 
                    itemGrid.Children.Add(imageLabel);

                    // 목록 이름
                    Label nameLabel = new Label()
                    {
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        VerticalTextAlignment=TextAlignment.Center,
                        //TextColor = Color.Black,
                       //BackgroundColor=Color.SteelBlue,
                        FontSize = Device.GetNamedSize(NamedSize.Body, typeof(Label))
                    };

                    // 목록 이름과 색상을 바인딩
                    nameLabel.SetBinding(Label.TextProperty, "name");
                    //nameLabel.SetBinding(Label.TextColorProperty, "color"); // 목록 이름 색상 설정
                    Grid.SetColumn(nameLabel, 1);  // 0열, 1행에 위치
                    Grid.SetRowSpan(nameLabel, 2); // 2열 차지(중앙에 위치 위해서)
                    Grid.SetColumnSpan(nameLabel, 2); // 2행 차지(중앙에 위치 위해서)
                    itemGrid.Children.Add(nameLabel);

                   // itemGrid.SetBinding(Grid.BackgroundColorProperty, "color");

                    

                    Frame gridFrame = new Frame
                    {
                        BorderColor = Color.FromHex("#EEEEEE"),
                        HasShadow=false,
                        Content=itemGrid,
                        CornerRadius=0
                    };

                    // 목록 클릭 이벤트 설정 (TapGestureRecognizer)
                    var tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += (s, e) =>
                    {
                        //gridFrame.BackgroundColor = Color.Orange;
                        OnCollcetionViewItemSelected(nameLabel.Text, (FileImageSource)image.Source); // 목록 이름과 아이콘
                    };
                    gridFrame.GestureRecognizers.Add(tapGestureRecognizer); // Grid에 만든 탭제스처 추가

                    return gridFrame;
                })
                /* ItemTemplate 끝 */
            };
            /* collectionView 끝 */

            // 목록 검색창
            var searchBar = new Xamarin.Forms.SearchBar
            {
                Margin=0,
                Placeholder = "Search items...",
                PlaceholderColor = Color.Silver,
                TextColor = Color.Black,
                //HorizontalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Xamarin.Forms.SearchBar)),
                // FontAttributes = FontAttributes.Italic
            };

            // 검색창 텍스트 입력시 이벤트
            searchBar.TextChanged += (sender, e) =>
            {
                collectionView.ScrollTo(0); //검색내용 맨 위부터 보이게
                //Xamarin.Forms.SearchBar searchBar2 = (Xamarin.Forms.SearchBar)sender;
                collectionView.ItemsSource = GetSearchResults(e.NewTextValue);
            };

            modalLayout.Children.Add(searchBar); // 피커 목록 검색창
            modalLayout.Children.Add(collectionView); // 피커 목록

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
                MessagingCenter.Send<MainPage2_PickerPage, string> (this, "text", text); // 목록 이름
                MessagingCenter.Send(this, "source", source); // 목록 아이콘

                await Navigation.PopModalAsync();
            }
        }

        // 목록 검색 함수
        private List<CustomPickerItems> GetSearchResults(string queryString)
        {
            var normalizedQuery = queryString?.ToLower() ?? ""; // 앞에 값이 null이면 뒤에 있는 "" 대입
            return CustomPickerViewModel.CustomPickerItems.Where(f => f.name.ToLowerInvariant().Contains(normalizedQuery)).ToList();
        }

    }
}


/*******************/
/*                 */
/*                 */
/* 이하 쓰레기통 입니다 */
/*                 */
/*                 */
/*******************/


/*
// 목록간 구분선 정의
var separator = new BoxView
{
    HeightRequest = 1,
    BackgroundColor = Color.White,
    VerticalOptions = LayoutOptions.End
};
//itemGrid.Children.Add(separator);
Grid.SetColumnSpan(separator, 3); // ColumnSpan을 통해 일자로 쭉
*/

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

