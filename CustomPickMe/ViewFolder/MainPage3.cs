using Xamarin.Forms;

namespace CustomPickMe.ViewFolder
{
    public class MainPage3 : ContentPage
    {
        private Button _defaultButton;
        private Button _startButton;
        private Button _centerButton;
        private Button _endButton;
        private Button _fillButton;
        
        public MainPage3()
        {
            _defaultButton=new Button
            {
                Text="default"
            };
            _startButton = new Button
            {
                Text="Start",
                HorizontalOptions = LayoutOptions.Start
            };
            _centerButton = new Button
            {
                Text="Center",
                HorizontalOptions = LayoutOptions.Center
            };
            _endButton = new Button
            {
                Text="End",
                HorizontalOptions = LayoutOptions.End
            };
            _fillButton = new Button
            {
                Text="Fill",
                HorizontalOptions = LayoutOptions.Fill,
                
            };

            MyLayout myLayout = new MyLayout()
            {
                Children =
                {
                    _defaultButton,_startButton,_centerButton,_endButton,_fillButton
                }
            
            };
            Content = myLayout;

        }

    }
}