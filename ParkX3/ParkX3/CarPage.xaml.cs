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
		}
	}
}
