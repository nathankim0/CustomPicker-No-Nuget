using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace CustomPicker
{
    public static class CustomPickerData
    {
        public static ObservableCollection<CustomPickerItems> GetListDatas()
        {
            ObservableCollection<CustomPickerItems> customPickerItems = new ObservableCollection<CustomPickerItems>()
            {
                new CustomPickerItems("General", Color.Red),
                new CustomPickerItems("Health", Color.Orange),
                new CustomPickerItems("Career", Color.Yellow),
                new CustomPickerItems("Relationship", Color.Green),
                new CustomPickerItems("Learning", Color.Blue),
                new CustomPickerItems("Enjoyment", Color.DarkBlue),
                new CustomPickerItems("Wealth", Color.Purple),
                new CustomPickerItems("Travel", Color.Black)
            };

            return customPickerItems;
        }
    }
}