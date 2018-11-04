using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Pixo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ForgetPage : Page
    {
        public ForgetPage()
        {
            this.InitializeComponent();
            var currentView = SystemNavigationManager.GetForCurrentView();
            currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            currentView.BackRequested += CurrentView_BackRequested;
        }

        private void CurrentView_BackRequested(object sender, BackRequestedEventArgs e)
        {
            e.Handled = true;
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }



        private async void btnGo_Click(object sender, RoutedEventArgs e)
        {
            btnGo.IsEnabled = false;
            tbEmail.IsEnabled = false;
            var userSession = new UserSessionData
            {
                UserName = "dsfsdf",
                Password = "dsfsdfdsfdsf"
            };

            var Api = InstaApiBuilder.CreateBuilder()
                .SetUser(userSession)
                .Build();

            StringChecker checker = new StringChecker();
            var isMail = checker.IsValidEmail(tbEmail.Text);
            if (isMail)
            {
                var result = await Api.SendRecoveryByEmailAsync(tbEmail.Text);
                if (result.Succeeded)
                {
                    Statico.Notifer.Show("We send a Link to your Email.", Statico.NotifDelay);
                    Frame.Navigate(typeof(Login));
                }
                else
                {
                    Statico.Notifer.Show("Error: " + result.Info.Message, Statico.NotifDelay);
                }
            }
            else
            {
                var result = await Api.SendRecoveryByUsernameAsync(tbEmail.Text);
                if (result.Succeeded)
                {
                    Statico.Notifer.Show(result.Value.Body, Statico.NotifDelay);
                    Frame.Navigate(typeof(Login));
                }
                else
                {
                    Statico.Notifer.Show("Error: " + result.Info.Message, Statico.NotifDelay);
                }
            }
            btnGo.IsEnabled = true;
            tbEmail.IsEnabled = true;
        }

        private void tbForget_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(ForgetPhone));
        }
    }
}
