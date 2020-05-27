using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Calculator
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        public MainPage(CalculatorViewModel viewModel)
        {
            
            InitializeComponent();
            BindingContext = viewModel;
        }
        private void SecondClicked(object sender, EventArgs e)
        {
            if (count % 2 == 0)
            {
                changeButton.Text = "2^x";
                second.BackgroundColor = Color.Gray;
            }
            else
            {
                changeButton.Text = "10^x";
                second.BackgroundColor = Color.FromHex("#1f1f1f");
            }
            count++;
        }
        }
}
