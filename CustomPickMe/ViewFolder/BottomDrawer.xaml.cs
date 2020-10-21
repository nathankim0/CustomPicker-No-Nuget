using System;
using System.Collections.Generic;
using CustomPickMeMain;
using Xamarin.Forms;
using CustomPicker;
using System.Linq;

namespace CustomPickMe.ViewFolder
{
    public partial class BottomDrawer : ContentPage
    {
        public BottomDrawer()
        {
            InitializeComponent();


            mycollectionview mycollectionview = new mycollectionview();
            collectionStack.Children.Add(mycollectionview.stackLayout);

            BindingContext = new MainPageItem();
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            ((MainPageItem)BindingContext).IsVisible = true;
        } 
    }
}
