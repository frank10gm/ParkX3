using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ParkX3
{
    public partial class CarPage : ContentPage
    {
        public CarPage(Car select, string marca)
        {
            InitializeComponent();
            this.Title = select.modello;
            modello.Text = marca + " " + select.modello + Environment.NewLine;
            breveDesc.Text = select.breveDesc;

            Frame frame = new Frame
            {
                Content = new Label { Text = "I'm Framous!" },
                OutlineColor = Color.Silver,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center
            };
        }

    }
}
