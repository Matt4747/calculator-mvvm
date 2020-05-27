using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Calculator
{
    public partial class App : Application
    {
        CalculatorViewModel calculatorViewModel;


        public App()
        {
            InitializeComponent();

            calculatorViewModel = new CalculatorViewModel();
            calculatorViewModel.RestoreState(Current.Properties);
            MainPage = new MainPage(calculatorViewModel);

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
