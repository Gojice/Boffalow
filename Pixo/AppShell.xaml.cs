using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Toolkit.Uwp.UI.Animations;
using Pixo.Views;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Pixo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AppShell : Page
    {
        public static Frame framer;
        private Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public AppShell()
        {
            this.InitializeComponent();
        }

        private void Page_Loading(FrameworkElement sender, object args)
        {
            Statico.Notifer = Notifer;
            framer = myFrame;
            //  await myFrame.Blur(7, 500, 0).StartAsync();
            if (localSettings.Values["ihaveaaccount"] != null)
            {
                if (localSettings.Values["ihaveaaccount"].ToString() == "yes")
                {
                    myFrame.Navigate(typeof(HomePage));
                }
                else
                {
                    myFrame.Navigate(typeof(LoginPage));
                }
            }
            else
            {
                myFrame.Navigate(typeof(LoginPage));
            }
        }
    }
}
