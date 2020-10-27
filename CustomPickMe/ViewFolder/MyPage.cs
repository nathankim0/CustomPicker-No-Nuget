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
        StackLayout _bottomSheetFrame_bottomSheetGestureAreaStackLayout;

        public MyPage()
        {
            AbsoluteLayout absoluteLayout = new AbsoluteLayout();

            mycollectionview = new mycollectionview();

            _bottomSheetFrame = new Frame
            {
                HasShadow = true,
                CornerRadius = 20
            };

            BoxView box = new BoxView
            {
                Margin = 5,
                HeightRequest = 4,
                CornerRadius = 5,
                WidthRequest = 70,
                BackgroundColor = Color.Gray,
                HorizontalOptions = LayoutOptions.Center
            };
            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += OnPanUpdated;
            _bottomSheetFrame.GestureRecognizers.Add(panGesture);


            mycollectionview.stackLayout.Padding = 10;

            _bottomSheetStackLayout = new StackLayout
            {
                Children = { box, mycollectionview.stackLayout }
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
                        Console.WriteLine("@@@@ Up");
                        Console.WriteLine("@@@@ y: " + y);

                        if (y > Height / 2 - 50)
                        {
                            y = Height / 2 - 50;
                            _bottomSheetFrame.TranslateTo(_bottomSheetFrame.X, y, 100);
                            mycollectionview.collectionView.HeightRequest = Height- y - 150;
                        }
                        else
                        {
                            y = 0;
                            _bottomSheetFrame.TranslateTo(_bottomSheetFrame.X, y, 100);
                            mycollectionview.collectionView.HeightRequest = Height - 150;
                        }

                    }
                    else if (down == true)
                    {
                        Console.WriteLine("@@@@ Down");
                        Console.WriteLine("@@@@ y: "+ y);
                        y = Height - 100;
                        _bottomSheetFrame.TranslateTo(_bottomSheetFrame.X, y, 150);
                    }

                    break;
            }
        }
    }
}

