using System;
using System.Collections.Generic;
using System.Linq;
using CustomPicker;
using Xamarin.Forms;

namespace CustomPickMe
{
    public class mycollectionview
    {
        public CollectionView collectionView;
        public StackLayout stackLayout;
        //public Xamarin.Forms.SearchBar searchBar;
        public Entry searchBar;

        public mycollectionview()
        {
            /* collectionView 시작 */
            collectionView = new CollectionView   // Picker가 될 Collection View
            {
                EmptyView = "없네요 ㅎ", // 데이터 없을때
                Margin = new Thickness(0, 0, 0, 20), // 아이폰 네비게이션바 안겹치게 
                ItemsSource = CustomPickerViewModel.CustomPickerItems, // 목록 항목들
                VerticalOptions = LayoutOptions.FillAndExpand,

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
                        Aspect = Aspect.AspectFit,
                        HeightRequest = 52,
                        WidthRequest = 52
                    };
                    image.SetBinding(Image.SourceProperty, "imagesource");
                    itemGrid.Children.Add(image, 0, 0);

                    // 아이콘 설명 라벨
                    Label imageLabel = new Label()
                    {
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Start,
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
                        VerticalTextAlignment = TextAlignment.Center,
                        FontSize = Device.GetNamedSize(NamedSize.Body, typeof(Label))
                    };

                    // 목록 이름과 색상을 바인딩
                    nameLabel.SetBinding(Label.TextProperty, "name");
                    Grid.SetColumn(nameLabel, 1);  // 0열, 1행에 위치
                    Grid.SetRowSpan(nameLabel, 2); // 2열 차지(중앙에 위치 위해서)
                    itemGrid.Children.Add(nameLabel);


                    Frame gridFrame = new Frame
                    {
                        HasShadow = false,
                        Content = itemGrid,
                        CornerRadius = 0
                    };

                    // 목록 클릭 이벤트 설정 (TapGestureRecognizer)
                    var tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += (s, e) =>
                    {
                        OnCollcetionViewItemSelected(nameLabel.Text, (FileImageSource)image.Source); // 목록 이름과 아이콘
                    };
                    gridFrame.GestureRecognizers.Add(tapGestureRecognizer); // Grid에 만든 탭제스처 추가

                    return gridFrame;
                })
                /* ItemTemplate 끝 */
            };
            /* collectionView 끝 */

            // 목록 검색창
            searchBar = new Entry
            {
                Margin = 10,
                Placeholder = "Search items...",
                BackgroundColor = Color.FromHex("#D3D3D3"),
                //PlaceholderColor = Color.Silver,
                //TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Editor)),
                HeightRequest = 40,
                ClearButtonVisibility=ClearButtonVisibility.WhileEditing
            };

            /*
            // 목록 검색창
            searchBar = new Xamarin.Forms.SearchBar
            {
                Margin = 10,
                Placeholder = "Search items...",
                //PlaceholderColor = Color.Silver,
                //TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Xamarin.Forms.SearchBar)),
                HeightRequest = 30
            };
            */
            // 검색창 텍스트 입력시 이벤트
            searchBar.TextChanged += (sender, e) =>
            {
                collectionView.ScrollTo(0); //검색내용 맨 위부터 보이게
                //Xamarin.Forms.SearchBar searchBar2 = (Xamarin.Forms.SearchBar)sender;
                collectionView.ItemsSource = GetSearchResults(e.NewTextValue);
            };
            stackLayout = new StackLayout
            {
                Children =
                {
                    searchBar,
                    collectionView
                }
            };
        }
        // 목록 선택 함수
        private void OnCollcetionViewItemSelected(string text, string source)
        {
            if (text != null && text != "")
            {
                // Messaging Center를 통해 Publish a Message.
                MessagingCenter.Send(this, "text", text); // 목록 이름
                MessagingCenter.Send(this, "source", source); // 목록 아이콘
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
