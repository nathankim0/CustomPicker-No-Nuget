using System;
using System.Collections.Generic;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms;

namespace CustomPickMe.View
{
    public partial class IosModalPage : ContentPage
    {
        public IosModalPage()
        {
            InitializeComponent();
            //On<iOS>().SetModalPresentationStyle(UIModalPresentationStyle.Automatic);
        }
    }
}