using CustomPickMe;
using StatusBarApp.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(StatusBar))]
namespace StatusBarApp.iOS
{
    class StatusBar : IStatusBar
    {
        public int GetHeight()
        {
            return (int)UIApplication.SharedApplication.StatusBarFrame.Height;
        }
    }
}