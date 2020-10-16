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
                new CustomPickerItems("General", Color.Red, "apple"),
                new CustomPickerItems("Health", Color.Orange,"facetime.png"),
                new CustomPickerItems("Career", Color.Black,"mac"),
                new CustomPickerItems("Relationship", Color.Green,"maps"),
                new CustomPickerItems("Learning", Color.Blue,"shortcuts"),
                new CustomPickerItems("Enjoyment", Color.DarkBlue,"facetime"),
                new CustomPickerItems("Wealth", Color.Gold,"apple"),
                new CustomPickerItems("긴글씨\n가나다라마바사아자차카타파하ABCDEFGHIJKLMNOPQRSTUVWXYZ", Color.Tomato,"facetime"),
                new CustomPickerItems("Wealth", Color.Tan,"mac"),
                new CustomPickerItems("Wealth", Color.SlateGray,"maps"),
                new CustomPickerItems("Wealth", Color.MediumVioletRed,"shortcuts"),
                new CustomPickerItems("Wealth", Color.LightBlue,"facetime"),
                new CustomPickerItems("General", Color.Red, "apple"),
                new CustomPickerItems("Health", Color.Orange,"facetime.png"),
                new CustomPickerItems("Career", Color.Black,"mac"),
                new CustomPickerItems("Relationship", Color.Green,"maps"),
                new CustomPickerItems("Learning", Color.Blue,"shortcuts"),
                new CustomPickerItems("Enjoyment", Color.DarkBlue,"facetime"),
                new CustomPickerItems("Wealth", Color.Gold,"apple"),
                new CustomPickerItems("긴글씨\n가나다라마바사아자차카타파하ABCDEFGHIJKLMNOPQRSTUVWXYZ", Color.Tomato,"facetime"),
                new CustomPickerItems("Wealth", Color.Tan,"mac"),
                new CustomPickerItems("Wealth", Color.SlateGray,"maps"),
                new CustomPickerItems("Wealth", Color.MediumVioletRed,"shortcuts"),
                new CustomPickerItems("Wealth", Color.LightBlue,"facetime"),
                new CustomPickerItems("General", Color.Red, "apple"),
                new CustomPickerItems("Health", Color.Orange,"facetime.png"),
                new CustomPickerItems("Career", Color.Black,"mac"),
                new CustomPickerItems("Relationship", Color.Green,"maps"),
                new CustomPickerItems("Learning", Color.Blue,"shortcuts"),
                new CustomPickerItems("Enjoyment", Color.DarkBlue,"facetime"),
                new CustomPickerItems("Wealth", Color.Gold,"apple"),
                new CustomPickerItems("긴글씨\n가나다라마바사아자차카타파하ABCDEFGHIJKLMNOPQRSTUVWXYZ", Color.Tomato,"facetime"),
                new CustomPickerItems("Wealth", Color.Tan,"mac"),
                new CustomPickerItems("Wealth", Color.SlateGray,"maps"),
                new CustomPickerItems("Wealth", Color.MediumVioletRed,"shortcuts"),
                new CustomPickerItems("Wealth", Color.LightBlue,"facetime"),

            };
            /*
            for (int i = 1; i <= 5; i++)
            {
                customPickerItems.Add(new CustomPickerItems("ADD"+i.ToString(), Color.Black, "apple"));
            }
            */
            return customPickerItems;
        }
    }
}