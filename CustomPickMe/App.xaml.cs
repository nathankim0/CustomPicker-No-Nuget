using CustomPickMe.View;
using Xamarin.Forms;

namespace CustomPickMe
{
    public partial class App : Application
    {
        public static double ScreenWidth;
        public static double ScreenHeight;
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new HomePage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}