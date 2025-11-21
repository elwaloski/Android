using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Prueba
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new HolaMundo();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
