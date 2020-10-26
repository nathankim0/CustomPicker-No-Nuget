using System;
using CustomPickMeMain;
using Xamarin.Forms;

namespace CustomPickMe.ViewFolder
{
    public class MyPage : ContentPage
    {
        double x;
        double y;
        Frame _bottomSheetFrame;
        StackLayout _bottomSheetStackLayout;
        mycollectionview mycollectionview;
        StackLayout _bottomSheetGestureAreaStackLayout;

        public MyPage()
        {
            AbsoluteLayout absoluteLayout = new AbsoluteLayout();

            mycollectionview = new mycollectionview();

            _bottomSheetFrame = new Frame
            {
                HasShadow = true,
                CornerRadius = 20
            };

            _bottomSheetGestureAreaStackLayout = new StackLayout
            {
                Children = {new BoxView
                {
                    Margin=20,
                    HeightRequest=8,
                    CornerRadius=6,
                    WidthRequest=70,
                    BackgroundColor=Color.Gray,
                    HorizontalOptions=LayoutOptions.Center
                } }
            };
            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += OnPanUpdated;
            _bottomSheetGestureAreaStackLayout.GestureRecognizers.Add(panGesture);


            mycollectionview.stackLayout.Padding = 10;

            _bottomSheetStackLayout = new StackLayout
            {
                Children = { _bottomSheetGestureAreaStackLayout, mycollectionview.stackLayout }
            };

            _bottomSheetFrame.Content = _bottomSheetStackLayout;

            absoluteLayout.Children.Add(_bottomSheetFrame);

            _bottomSheetFrame.TranslateTo(_bottomSheetFrame.X, 300, 20);
            y = 300;

            //mycollectionview.collectionView.HeightRequest = App.ScreenHeight-250;

            BindingContext = new MainPageItem();

            Content = absoluteLayout;
        }

        void Button_Clicked(Object sender, EventArgs e)
        {
            ((MainPageItem)BindingContext).IsVisible = true;
            Console.WriteLine("**** button click");
        }

        bool up, down;

        void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    
                    double translateY;
                    double toY = y + e.TotalY;

                    if (Device.RuntimePlatform == Device.Android)
                    {
                        toY = y + e.TotalY * 5.0;
                    }
                    translateY = Math.Max(toY,0); 
                    _bottomSheetFrame.TranslateTo(_bottomSheetFrame.X, translateY, 100);

                    //mycollectionview.collectionView.HeightRequest = Height - translateY - 150;


                    Console.WriteLine("**** translateY " + translateY);
                    Console.WriteLine("**** e.TotalY " + e.TotalY);
                    Console.WriteLine("**** Height " + Height);

                    if (e.TotalY < 0)
                    {
                        down = false;
                        up = true;
                    }
                    else
                    {
                        up = false;
                        down = true;
                    }


                    break;

                case GestureStatus.Completed:
                    y = _bottomSheetFrame.TranslationY;

                    if (up == true)
                    {

                        if (y < Height - 150 && y > Height / 2 - 60)
                        {
                            y = Height / 2 - 60;
                            _bottomSheetFrame.TranslateTo(_bottomSheetFrame.X, y, 100);
                        }

                        else if (y > Height - 150)
                        {
                            ((MainPageItem)BindingContext).IsVisible = false;
                            y = Height - 150;
                        }

                        else if (y < Height / 2 - 60)
                        {
                            y = 100;
                            _bottomSheetFrame.TranslateTo(_bottomSheetFrame.X, y, 100);
                        }
                    }
                    else if (down == true)
                    {
                        if (y < Height - 150 && y > Height / 2)
                        {
                            y = Height - 150;
                            _bottomSheetFrame.TranslateTo(_bottomSheetFrame.X, y, 100);
                        }

                        else if (y > Height - 150)
                        {
                            ((MainPageItem)BindingContext).IsVisible = false;
                            y = Height - 150;
                        }

                        else if (y < Height / 2)
                        {
                            y = Height / 2 - 60;
                            _bottomSheetFrame.TranslateTo(_bottomSheetFrame.X, y, 100);
                        }
                    }

                    break;
            }
        }
    }
}

