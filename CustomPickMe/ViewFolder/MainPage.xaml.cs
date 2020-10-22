using System;
using System.ComponentModel;
using CustomPickMeMain;
using Xamarin.Forms;
using Xamarin.Essentials;
using PanGesture;

namespace CustomPickMe.ViewFolder
{
    public partial class MainPage : ContentPage
    {
        double x;
        double y;
        Frame _bottomSheetFrame;
       // StackLayout _bottomSheetStackLayout;
        mycollectionview mycollectionview;
        StackLayout _bottomSheetGestureAreaStackLayout;


        public MainPage()
        {
            mycollectionview = new mycollectionview();

            _bottomSheetGestureAreaStackLayout = new StackLayout
            {
                Children =
                {
                    new BoxView
                    {
                    Margin=20,
                    HeightRequest=8,
                    CornerRadius=6,
                    WidthRequest=70,
                    BackgroundColor=Color.Gray,
                    HorizontalOptions=LayoutOptions.Center
                    },
                    mycollectionview.stackLayout
                }
            };

            /*
            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += OnPanUpdated;
            _bottomSheetGestureAreaStackLayout.GestureRecognizers.Add(panGesture);
            */

            /*
            _bottomSheetStackLayout = new StackLayout
            {
                Children = { _bottomSheetGestureAreaStackLayout, mycollectionview.stackLayout }
            };
            */

            _bottomSheetFrame = new Frame
            {
                HasShadow = true,
                CornerRadius = 20,
                Content = _bottomSheetGestureAreaStackLayout
            };
            //_bottomSheetFrame.SetBinding(Frame.IsVisibleProperty, "IsVisible");

            //bottomSheet.TranslateTo(bottomSheet.X, -300, 20);
            //y = -300;

            BindingContext = new MainPageItem();

            RelativeLayout relativeLayout = new RelativeLayout();
            relativeLayout.BackgroundColor = Color.FromHex("#FFB822");

            /*
            Button categoryButton = new Button
            {
                Text="category button"
            };
            categoryButton.Clicked += Button_Clicked;


            relativeLayout.Children.Add(
                categoryButton,
                Constraint.RelativeToParent((parent) =>
                {
                    return parent.X +50;
                }),
            Constraint.RelativeToParent((parent) =>
            {
                return parent.Y + 50;
            }),
            Constraint.Constant(180),
            Constraint.Constant(100));
            */

            relativeLayout.Children.Add(
                new PanContainer
            {
                Content = _bottomSheetFrame
            },
            Constraint.RelativeToParent((parent) =>
            {
                return parent.X;
            }),
            Constraint.RelativeToParent((parent) =>
            {
                return parent.Y * .8;
            }),
            Constraint.RelativeToParent((parent) =>
            {
                return parent.Width;
            }),
            Constraint.RelativeToParent((parent) =>
            {
                return parent.Height;
            }));
            Content = relativeLayout;

            

            /*
            RelativeLayout relativeLayout1 = new RelativeLayout();
            relativeLayout1.BackgroundColor = Color.Black;

            relativeLayout1.Children.Add(
                new PanContainer
                {
                    Content = new Label
                    {
                        Text = "Hello",
                        BackgroundColor = Color.Aqua,
                        WidthRequest = 300,
                        HeightRequest = 300
                    }
                },
            Constraint.RelativeToParent((parent) =>
            {
                return parent.X;
            }),
            Constraint.RelativeToParent((parent) =>
            {
                return parent.Y * .8;
            }),
            Constraint.RelativeToParent((parent) =>
            {
                return parent.Width;
            }),
            Constraint.RelativeToParent((parent) =>
            {
                return parent.Height;
            }));

            Content = relativeLayout1;*/
        }


        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            ((MainPageItem)BindingContext).IsVisible = true;
            Console.WriteLine("**** button click");

        }

        void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    var translateY = Math.Max(y + e.TotalY, -Math.Abs((Height * 0.1) - Height));
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