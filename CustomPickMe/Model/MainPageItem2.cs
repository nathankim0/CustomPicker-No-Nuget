using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CustomPicker2
{
    public class MainPageItem2 : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string _selected2="Category";

		public string Selected2
		{
			get	{ return _selected2;}
			set
			{
				_selected2 = value;
				OnPropertyChanged();
			}
		}
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

