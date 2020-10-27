using Android.App;
using CustomPickMe;
using StatusBarApp.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(StatusBar))]
namespace StatusBarApp.Droid
{
    class StatusBar : IStatusBar
    {
        public static Activity Activity { get; set; }

        public int GetHeight()
        {
            int statusBarHeight = -1;
            int resourceId = Activity.Resources.GetIdentifier("status_bar_height", "dimen", "android");
            if (resourceId > 0)
            {
                statusBarHeight = Activity.Resources.GetDimensionPixelSize(resourceId);
            }
            return statusBarHeight;
        }
    }
}