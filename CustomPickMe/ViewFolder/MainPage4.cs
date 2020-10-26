using System;
using CustomPickMeMain;
using Xamarin.Forms;
using PanGesture;

namespace CustomPickMe.ViewFolder
{
    public partial class MainPage4 : ContentPage
    {
        Frame _bottomSheetFrame;
        mycollectionview _myCollectionview;
        StackLayout _bottomSheetGestureAreaStackLayout;
        StackLayout _bottomSheetInnerStackLayout;
        public MainPage4()
        {
            _myCollectionview = new mycollectionview();

            _bottomSheetGestureAreaStackLayout = new StackLayout
            {
                Children =
                {
                    new BoxView
                    {
                    Margin=10,
                    HeightRequest=6,
                    CornerRadius=4,
                    WidthRequest=100,
                    BackgroundColor=Color.Gray,
                    HorizontalOptions=LayoutOptions.Center
                    }
                }
            };
            _bottomSheetInnerStackLayout = new StackLayout
            {
                Children =
                {
                    _bottomSheetGestureAreaStackLayout, _myCollectionview.stackLayout
                }
            };

            _bottomSheetFrame = new Frame
            {
                HasShadow = true,
                CornerRadius = 20,
                Content = _bottomSheetInnerStackLayout
            };
            _bottomSheetFrame.SetBinding(IsVisibleProperty, "IsVisible");
            BindingContext = new MainPageItem();

            RelativeLayout relativeLayout = new RelativeLayout();
            relativeLayout.BackgroundColor = Color.FromHex("#FFB822");


            Button categoryButton = new Button
            {
                Text="category button"
            };
            categoryButton.Clicked += Button_Clicked;

            /*
            AbsoluteLayout absoluteLayout = new AbsoluteLayout();
            absoluteLayout.Children.Add(categoryButton);
            absoluteLayout.Children.Add(new PanContainer
            {
                Content = _bottomSheetFrame
            });
            Content = absoluteLayout;
            */
            
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
          
            relativeLayout.Children.Add(
                new PanContainer
            {
                Content = _bottomSheetFrame
            }, yConstraint: Constraint.RelativeToParent(parent=>parent.Y*.9), widthConstraint:Constraint.RelativeToParent(parent=>parent.Width),heightConstraint:Constraint.RelativeToParent(parent=>parent.Height));
            
            Content = relativeLayout;
            
            
        }

        void Button_Clicked(Object sender, EventArgs e)
        {
            ((MainPageItem)BindingContext).IsVisible = true;
            Console.WriteLine("**** button click");
        }

    }
}