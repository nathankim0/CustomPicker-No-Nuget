using CustomPickMe.View;
using Xamarin.Forms;

namespace CustomPickMe
{
    public partial class App : Application
    {
        public static int screenHeight, screenWidth;

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