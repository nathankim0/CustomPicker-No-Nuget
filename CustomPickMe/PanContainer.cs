using System;
using CustomPickMe;
using CustomPickMeMain;
using Xamarin.Forms;

namespace PanGesture
{
	public class PanContainer : ContentView
	{
		double x, y;

		public PanContainer()
		{
            BackgroundColor = Color.Accent;
			var panGesture = new PanGestureRecognizer();
			panGesture.PanUpdated += OnPanUpdated;
			GestureRecognizers.Add(panGesture);
		}

		void OnPanUpdated(object sender, PanUpdatedEventArgs e)
		{
			switch (e.StatusType)
			{
				case GestureStatus.Running:
					Console.WriteLine("**** GestureStatus.Running: ");
                    Console.WriteLine("**** Height: " + Height);
                    Console.WriteLine("**** App.ScreenHeight: " + App.ScreenHeight);

                    //Content.TranslationY = Math.Max(y + e.TotalY, 0);

                    //Content.TranslationY=Math.Max(Math.Min(0, y + e.TotalY), -Math.Abs((Height * .25) - Height));
                    Content.TranslationY = y + e.TotalY;

                    Console.WriteLine("**** Content.TranslationY: " + Content.TranslationY);

					break;

				case GestureStatus.Completed:
					Console.WriteLine("**** GestureStatus.Completed: ");

                    y = Content.TranslationY;


                    /*
					if (y > Height-100)
					{
						((MainPageItem)BindingContext).IsVisible = false;
					}
                    */


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
