using System;
using System.ComponentModel;
using CustomPickMeMain;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace CustomPickMe.ViewFolder
{
    public partial class MainPage : ContentPage
    {
        double x;
        double y;
        Frame _bottomSheetFrame;
        StackLayout _bottomSheetStackLayout;
        mycollectionview mycollectionview;
        StackLayout _bottomSheetGestureAreaStackLayout;


        public MainPage()
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



            _bottomSheetStackLayout = new StackLayout
            {
                Children = { _bottomSheetGestureAreaStackLayout, mycollectionview.stackLayout }
            };

            _bottomSheetFrame.Content = _bottomSheetStackLayout;

            absoluteLayout.Children.Add(_bottomSheetFrame);

            //bottomSheet.TranslateTo(bottomSheet.X, -300, 20);
            //y = -300;
            BindingContext = new MainPageItem();

            Content = absoluteLayout;
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            //((MainPageItem)BindingContext).IsVisible = true;

        }

        void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Running:

                    double translateY;

                    double toY = y + e.TotalY;

                    translateY = Math.Max(toY, -Math.Abs((Height * 0.1) - Height));
                    _bottomSheetFrame.TranslateTo(_bottomSheetFrame.X, translateY, 20);

                    break;

                case GestureStatus.Completed: // 손 뗐을 때

                    y = _bottomSheetFrame.TranslationY;
                    x = _bottomSheetFrame.TranslationX;

                    Console.WriteLine("**** GestureStatus.Completed");
                    Console.WriteLine("**** bottomSheet.TranslationY: " + y);


                    if (y > -10)
                    {
                        ((MainPageItem)BindingContext).IsVisible = false;
                    }

                    //at the end of the event - snap to the closest location
                    //var finalTranslation = Math.Max(Math.Min(0, -1000), -Math.Abs(getClosestLockState(e.TotalY + y)));

                    /*
                    //depending on Swipe Up or Down - change the snapping animation
                    if (isSwipeUp(e))
                    {
                        bottomSheet.TranslateTo(bottomSheet.X, finalTranslation, 250, Easing.SinIn);
                    }
                    else
                    {
                        bottomSheet.TranslateTo(bottomSheet.X, finalTranslation, 250, Easing.SinOut);
                    }

                    //dismiss the keyboard after a transition
                    y = bottomSheet.TranslationY;
                    */

                    break;

            }

        }
        /*
        public bool isSwipeUp(PanUpdatedEventArgs e)
        {
            if (e.TotalY < 0)
            {
                Console.WriteLine("**** isSwipeUp e.TotalY < 0 ****");

                return true;
            }
            Console.WriteLine("**** isSwipeUp e.TotalY >= 0 ****");

            return false;
        }

        //TO-DO: Make this cleaner
        public double getClosestLockState(double TranslationY)
        {
            //Play with these values to adjust the locking motions - this will change depending on the amount of content ona  apge
            var lockStates = new double[] { 0, .5, .85 };

            //get the current proportion of the sheet in relation to the screen
            var distance = Math.Abs(TranslationY);
            var currentProportion = distance / Height;

            //calculate which lockstate it's the closest to
            var smallestDistance = 10000.0;
            var closestIndex = 0;
            for (var i = 0; i < lockStates.Length; i++)
            {
                var state = lockStates[i];
                var absoluteDistance = Math.Abs(state - currentProportion);
                if (absoluteDistance < smallestDistance)
                {
                    smallestDistance = absoluteDistance;
                    closestIndex = i;
                }
            }

            var selectedLockState = lockStates[closestIndex];
            var TranslateToLockState = getProportionCoordinate(selectedLockState);

            return TranslateToLockState;
        }

        public double getProportionCoordinate(double proportion)
        {
            return proportion * Height;
        }

        void dismissBottomSheet()
        {

            var finalTranslation = Math.Max(Math.Min(0, -1000), -Math.Abs(getProportionCoordinate(0)));
            bottomSheet.TranslateTo(bottomSheet.X, finalTranslation, 450, Easing.SpringOut);
        }

        void openBottomSheet()
        {
            var finalTranslation = Math.Max(Math.Min(0, -1000), -Math.Abs(getProportionCoordinate(.85)));
            bottomSheet.TranslateTo(bottomSheet.X, finalTranslation, 150, Easing.SpringIn);
        }
    }
        */
    }
}