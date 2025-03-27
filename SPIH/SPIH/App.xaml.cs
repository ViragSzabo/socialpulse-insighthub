using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using System;

namespace SPIH
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }
    }
}
