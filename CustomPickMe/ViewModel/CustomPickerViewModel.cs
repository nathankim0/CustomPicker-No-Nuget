using System.Collections.ObjectModel;

namespace CustomPicker
{
    public static class CustomPickerViewModel
    {
        public static ObservableCollection<CustomPickerItems> CustomPickerItems { get; }

        static CustomPickerViewModel()
        {
            CustomPickerItems = CustomPickerData.GetListDatas();
        }
    }
}