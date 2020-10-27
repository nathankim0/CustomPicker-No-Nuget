using System;
using CustomPickMe;
using CustomPickMeMain;
using Xamarin.Forms;
using CustomPickMe.ViewFolder;
using Xamarin.Essentials;

namespace PanGesture
{
	public class PanContainer : ContentView
	{
		double y;
		bool up, down;
		bool down2 = false;
		//Frame frame;
		mycollectionview _myCollectionview;
		BoxView _box;
		Frame _bottomSheetFrame;
		int statusBarHeight;
		double pageHeight;

		public PanContainer()
		{
            BackgroundColor = Color.FromHex("#50D3D3D3");
			var panGesture = new PanGestureRecognizer();
			panGesture.PanUpdated += OnPanUpdated;
			GestureRecognizers.Add(panGesture);

			statusBarHeight = DependencyService.Get<IStatusBar>().GetHeight();
			pageHeight = App.ScreenHeight - statusBarHeight;

			_myCollectionview = new mycollectionview();

			_box = new BoxView
			{
				Margin = 2,
				HeightRequest = 3,
				CornerRadius = 3,
				WidthRequest = 50,
				BackgroundColor = Color.FromHex("#D3D3D3"),
				HorizontalOptions = LayoutOptions.Center
			};

			_bottomSheetFrame = new Frame
			{
				CornerRadius = 20,
				Content = new StackLayout
				{
					Children =
					{
						_box,
						_myCollectionview.stackLayout
						, new StackLayout
                        {
							HeightRequest=100
                        }
					}
				}
			};
			y = pageHeight / 9;
			_bottomSheetFrame.TranslationY = y;

			_myCollectionview.collectionView.HeightRequest = pageHeight - 300;

			Content = _bottomSheetFrame;
		}

		void OnPanUpdated(object sender, PanUpdatedEventArgs e)
		{
			switch (e.StatusType)
			{
				case GestureStatus.Running:
					Console.WriteLine("**** GestureStatus.Running: ");
              

					_bottomSheetFrame.TranslationY = Math.Max(y + e.TotalY, 0);
					
                    Console.WriteLine("**** TranslationY: " + _bottomSheetFrame.TranslationY);
					Console.WriteLine("**** e.TotalY: " + e.TotalY);

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
                    if (e.TotalY >= 200)
                    {
						down2 = true;
                    }
                    else
                    {
						down2 = false;
                    }

					break;

				case GestureStatus.Completed:
					Console.WriteLine("**** GestureStatus.Completed: ");

                    y = Content.TranslationY;
					
					if (up == true)
					{
						Console.WriteLine("@@@@ Up");
						Console.WriteLine("@@@@ y: " + y);
						y = pageHeight / 9;
						_bottomSheetFrame.TranslateTo(_bottomSheetFrame.X, y, 100);
						/*
						if (y > pageHeight / 2)
						{
							y = pageHeight / 2;
							_bottomSheetFrame.TranslateTo(_bottomSheetFrame.X, y, 100);
							//_myCollectionview.collectionView.HeightRequest = Height - y - 150;

						}
						else
						{
							y = 0;
							_bottomSheetFrame.TranslateTo(_bottomSheetFrame.X, y, 100);
							//_myCollectionview.collectionView.HeightRequest = Height - 150;
						}*/
					}
					else if(down == true)
                    {
						Console.WriteLine("@@@@ Down");
						Console.WriteLine("@@@@ y: " + y);

						if (down2 == true)
                        {
							y = pageHeight;
							_bottomSheetFrame.TranslateTo(_bottomSheetFrame.X, y, 150);
							MessagingCenter.Send(this, "PanContainer_down");
							y = pageHeight / 9;
							_bottomSheetFrame.TranslateTo(_bottomSheetFrame.X, y, 20);
						}
                        else
                        {
							y = pageHeight / 9;
							_bottomSheetFrame.TranslateTo(_bottomSheetFrame.X, y, 100);
						}
                    }
					
					break;
			}
		}
    }
}
