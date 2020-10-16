using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace CustomPicker
{
    public class CustomPickerItems : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string _name;
		private Color _color;
		private string _imageSource;

		public string name
		{
			get	{ return _name;}
			set
			{
				_name = value;
				OnPropertyChanged();
			}
		}
        public Color color
		{
			get{ return _color;}
			set
			{
				_color = value;
				OnPropertyChanged();
			}
		}
		public string imagesource
		{
			get { return _imageSource; }
			set
			{
				_imageSource = value;
				OnPropertyChanged();
			}
		}
		public CustomPickerItems(string _name, Color _color)
		{
			this._name = _name;
			this._color = _color;
		}

		public CustomPickerItems (string _name, Color _color, string _imageSource)
		{
			this._name=_name;
			this._color=_color;
			this._imageSource = _imageSource;
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

