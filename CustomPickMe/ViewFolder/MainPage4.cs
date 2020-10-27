using System;
using CustomPickMeMain;
using Xamarin.Forms;
using PanGesture;

namespace CustomPickMe.ViewFolder
{
    public partial class MainPage4 : ContentPage
    {
        PanContainer panContainer;
        Button categoryButton;
        RelativeLayout relativeLayout;

        public MainPage4()
        {
            categoryButton = new Button
            {
                Text="category button"
            };
            categoryButton.Clicked += Button_Clicked;


            relativeLayout = new RelativeLayout();

            relativeLayout.Children.Add(
              categoryButton,
              Constraint.RelativeToParent((parent) =>
              {
                  return parent.X + 50;
              }),
              Constraint.RelativeToParent((parent) =>
              {
                  return parent.Y + 50;
              }));

            panContainer = new PanContainer();

            relativeLayout.Children.Add(panContainer,
            yConstraint: Constraint.RelativeToParent(parent => parent.Y));

            panContainer.IsVisible = false;

            MessagingCenter.Subscribe<PanContainer>(this, "PanContainer_down", (sender) =>
            {
                panContainer.IsVisible = false;
            });

            Content = relativeLayout;
            
        }

        void Button_Clicked(Object sender, EventArgs e)
        {
            panContainer.IsVisible = true;
            Console.WriteLine("**** button click");
        }

    }
}