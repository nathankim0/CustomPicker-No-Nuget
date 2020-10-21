using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CustomPickMeMain
{
    public class MainPageItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _selected = "Category";
        private bool _isVisible = false;

        public string Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                OnPropertyChanged();
            }
        }
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                OnPropertyChanged();
            }
        }
        /*
		public MainPageItems(string selected)
		{
			this._selected = selected;
		}*/

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}