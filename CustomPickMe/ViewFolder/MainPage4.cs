using System;
using Xamarin.Forms;
using CustomPicker;
using System.Collections.Generic;
using System.Linq;

///
/// Created by Jinyeob. 2020.10.21
/// Bottom Sheet Picker
/// 
namespace CustomPickMe.ViewFolder
{
    public class MainPage4 : ContentPage
    {
        private Grid _grid;
        private RelativeLayout _relativeLayout;
        private Button _categoryButton;
        private Grid _bottomSheetGrid;
        private StackLayout _backViewStackLayout;
        private CollectionView _collectionView;
        
        public MainPage4()
        {
            _grid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition
                    {
                        Height=GridLength.Star
                    }
                }
            };
            _relativeLayout=new RelativeLayout();
            Grid.SetRow(_relativeLayout,0);
            
            _backViewStackLayout = new StackLayout
            {
                Margin = new Thickness(10, 50, 10, 10),
                Children = 
                { 
                    _categoryButton, 
                    new Label
                    {
                        Text="Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."
                    },
                    new Label
                    {
                        Text="Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."
                    },
                    new Label
                    {
                        Text="Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."
                    },
                    new Label
                    {
                        Text="Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."
                    }
                }
            };
            
             /* collectionView 시작 */
             _collectionView = new CollectionView   // Picker가 될 Collection View
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
                _collectionView.ScrollTo(0); //검색내용 맨 위부터 보이게
                //Xamarin.Forms.SearchBar searchBar2 = (Xamarin.Forms.SearchBar)sender;
                _collectionView.ItemsSource = GetSearchResults(e.NewTextValue);
            };
            StackLayout listStackLayout = new StackLayout
            {
                Children = {searchBar, _collectionView}
            };
            
            _relativeLayout.Children.Add(_backViewStackLayout,Constraint.Constant(0),Constraint.Constant(0));
            
            _bottomSheetGrid = new Grid();
            
            var drawGestureRecognizer = new PanGestureRecognizer();
            drawGestureRecognizer.PanUpdated += PanGestureRecognizer_PanUpdated; 
            _bottomSheetGrid.GestureRecognizers.Add(drawGestureRecognizer);

            StackLayout _bottomSheetStackLayout = new StackLayout
            {
                Spacing = 6,
                Margin = new Thickness(0, 20, 0, 0),
                Children =
                {
                    new BoxView
                    {
                        HeightRequest = 5,
                        CornerRadius = 2,
                        WidthRequest = 50,
                        BackgroundColor=Color.Gray,
                        HorizontalOptions = LayoutOptions.Center
                    },
                    listStackLayout
                }
            };
            _bottomSheetGrid.Children.Add(_bottomSheetStackLayout);
            _relativeLayout.Children.Add(
                _bottomSheetGrid, 
                null,
                Constraint.RelativeToParent((parent) =>
            {
                return Width*0.93;
            }),
                Constraint.RelativeToParent((parent) =>
                {
                    return Width*1;
                }),
            Constraint.RelativeToParent((parent) =>
            {
                return Height*1;
            }));
            _grid.Children.Add(_relativeLayout);

            Content = _grid;

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            yHalfPosition = (App.screenHeight / 2) - 60;
            yFullPosition = App.screenHeight - 190;
            if (Device.RuntimePlatform == Device.iOS)
            {
                yFullPosition = App.screenHeight - (20 + 20 + 30 + 120);
            }
            yZeroPosition = 0;
            currentPsotion = 1;
            currentPostionY = yHalfPosition;
            _collectionView.HeightRequest = yHalfPosition;

            _bottomSheetGrid.TranslateTo(_bottomSheetGrid.X, -yHalfPosition);
        }

        double yHalfPosition;
        double yFullPosition;
        double yZeroPosition;
        int currentPsotion;
        double currentPostionY;
        bool up;
        bool down;
        bool isTurnY;
        double valueY;
        double y;
        void PanGestureRecognizer_PanUpdated(System.Object sender, Xamarin.Forms.PanUpdatedEventArgs e)
        {
            // Handle the pan
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    // Translate and ensure we don't y + e.TotalY pan beyond the wrapped user interface element bounds.
                    var translateY = Math.Max(Math.Min(0, y + e.TotalY), -Math.Abs((Height * .25) - Height));
                    //bottomSheet.TranslateTo(bottomSheet.X, -1*(currentPostionY+(-1*translateY)),20); //up working good



                    if (e.TotalY >= 5 || e.TotalY <= -5 && !isTurnY)
                    {
                        isTurnY = true;
                    }
                    if (isTurnY)
                    {
                        if (e.TotalY <= valueY)
                        {
                            up = true;

                        }
                        if (e.TotalY >= valueY)
                        {
                            down = true;

                        }
                    }
                    if (up)
                    {
                        if (Device.RuntimePlatform == Device.Android)
                        {
                            if (yFullPosition < (currentPostionY + (-1 * e.TotalY)))
                            {
                                _bottomSheetGrid.TranslateTo(_bottomSheetGrid.X, -yFullPosition);
                            }
                            else
                            {
                                _bottomSheetGrid.TranslateTo(_bottomSheetGrid.X, -1 * (currentPostionY + (-1 * e.TotalY)));
                            }
                        }
                        else
                        {
                            if (yFullPosition < (currentPostionY + (-1 * e.TotalY)))
                            {
                                _bottomSheetGrid.TranslateTo(_bottomSheetGrid.X, -yFullPosition, 20);
                            }
                            else
                            {
                                _bottomSheetGrid.TranslateTo(_bottomSheetGrid.X, -1 * (currentPostionY + (-1 * e.TotalY)), 20);
                            }
                        }
                    }
                    else if (down)
                    {
                        if (Device.RuntimePlatform == Device.Android)
                        {
                            if (yZeroPosition > currentPostionY - e.TotalY)
                            {
                                _bottomSheetGrid.TranslateTo(_bottomSheetGrid.X, -yZeroPosition);
                            }
                            else
                            {
                                _bottomSheetGrid.TranslateTo(_bottomSheetGrid.X, -(currentPostionY - (e.TotalY)));
                            }
                        }
                        else
                        {
                            if (yZeroPosition > currentPostionY - e.TotalY)
                            {
                                _bottomSheetGrid.TranslateTo(_bottomSheetGrid.X, -yZeroPosition, 20);
                            }
                            else
                            {
                                _bottomSheetGrid.TranslateTo(_bottomSheetGrid.X, -(currentPostionY - (e.TotalY)), 20);
                            }
                        }
                    }
                    break;
                case GestureStatus.Completed:
                    // Store the translation applied during the pan
                    valueY = e.TotalY;
                    y = _bottomSheetGrid.TranslationY;

                    //at the end of the event - snap to the closest location
                    //var finalTranslation = Math.Max(Math.Min(0, -1000), -Math.Abs(getClosestLockState(e.TotalY + y)));

                    //depending on Swipe Up or Down - change the snapping animation
                    if (up)
                    {
                        //swipe up happened
                        if (currentPsotion == 1)
                        {
                            _bottomSheetGrid.TranslateTo(_bottomSheetGrid.X, -yFullPosition);
                            currentPsotion = 2;
                            currentPostionY = yFullPosition;
                            _collectionView.HeightRequest = yFullPosition;
                            _collectionView.HeightRequest = yFullPosition;
                            //bottomSheet.TranslateTo(bottomSheet.X, finalTranslation, 250, Easing.SpringIn);
                        }
                        else if (currentPsotion == 0)
                        {
                            double currentY = (-1) * y;
                            double differentBetweenHalfAndCurrent = Math.Abs(currentY - yHalfPosition);
                            double differentBetweenFullAndCurrent = Math.Abs(currentY - yFullPosition);
                            //check which is close snap point and move to the closest snap point
                            if (differentBetweenHalfAndCurrent< differentBetweenFullAndCurrent)
                            {
                                //yHalfPosition is the closest one
                                _bottomSheetGrid.TranslateTo(_bottomSheetGrid.X, -yHalfPosition);
                                currentPsotion = 1;
                                currentPostionY = yHalfPosition;
                                _collectionView.HeightRequest = yHalfPosition;
                            }
                            else
                            {
                                _bottomSheetGrid.TranslateTo(_bottomSheetGrid.X, -yFullPosition);
                                currentPsotion = 2;
                                currentPostionY = yFullPosition;
                                _collectionView.HeightRequest = yFullPosition;
                            }
                            
                        }

                    }
                    if (down)
                    {
                        //swipe down happened
                        if (currentPsotion == 1)
                        {
                            _bottomSheetGrid.TranslateTo(_bottomSheetGrid.X, -yZeroPosition);
                            currentPsotion = 0;
                            currentPostionY = yZeroPosition;
                        }
                        else if (currentPsotion == 2)
                        {
                            double currentY = (-1) * y;
                            double differentBetweenHalfAndCurrent = Math.Abs(currentY - yHalfPosition);
                            double differentBetweenZeroAndCurrent = Math.Abs(currentY - yZeroPosition);
                            //check which is close snap point and move to the closest snap point
                            if (differentBetweenHalfAndCurrent < differentBetweenZeroAndCurrent)
                            {
                                //yHalfPosition is the closest one
                                _bottomSheetGrid.TranslateTo(_bottomSheetGrid.X, -yHalfPosition);
                                currentPsotion = 1;
                                currentPostionY = yHalfPosition;
                                _collectionView.HeightRequest = yHalfPosition;
                            }
                            else
                            {
                                _bottomSheetGrid.TranslateTo(_bottomSheetGrid.X, -yZeroPosition);
                                currentPsotion = 0;
                                currentPostionY = yZeroPosition;
                            }
                            

                        }
                        //bottomSheet.TranslateTo(bottomSheet.X, finalTranslation, 250, Easing.SpringOut);
                    }

                    //dismiss the keyboard after a transition
                    //SearchBox.Unfocus();
                    y = _bottomSheetGrid.TranslationY;
                    up = false;
                    down = false;
                    break;

            }
        }
        
        // 목록 선택 함수
        private async void OnCollcetionViewItemSelected(string text, string source)
        {
            if (text != null && text != "")
            {
                // Messaging Center를 통해 Publish a Message.
                //MessagingCenter.Send<MainPage2_PickerPage, string> (this, "text", text); // 목록 이름
                //MessagingCenter.Send(this, "source", source); // 목록 아이콘

                //await Navigation.PopModalAsync();
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
