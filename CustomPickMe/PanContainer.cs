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
			// Set PanGestureRecognizer.TouchPoints to control the 
			// number of touch points needed to pan
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

					// Translate and ensure we don't pan beyond the wrapped user interface element bounds.
					//Content.TranslationX = Math.Max(Math.Min(0, x + e.TotalX), -Math.Abs(Content.Width - App.ScreenWidth));
					//Content.TranslationY = Math.Max(y + e.TotalY, -Math.Abs((Height * 0.1) - Height));
					Content.TranslationY = y + e.TotalY;
					Console.WriteLine("**** Content.TranslationY: " + Content.TranslationY);
					//var translateY = Math.Max(Math.Min(0, y + e.TotalY), -Math.Abs((Height * 0.1) - Height));
					//Content.TranslateTo(Content.X, translateY, 20);
					break;

				case GestureStatus.Completed:
					Console.WriteLine("**** GestureStatus.Completed: ");
					// Store the translation applied during the pan
					//x = Content.TranslationX;
					y = Content.TranslationY;

					if (y > -10)
					{
						((MainPageItem)BindingContext).IsVisible = false;
					}

					break;
			}
		}
	}
}
