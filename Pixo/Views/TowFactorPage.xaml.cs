using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Pixo.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TowFactorPage : Page
    {
        private string username;
        private string password;
        bool canSendAgain = false;
        int Sec = 60;
        DispatcherTimer newTimer = new DispatcherTimer();

        public TowFactorPage()
        {
            this.InitializeComponent();
        }

        private async void tbresendCode_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (canSendAgain)
            {
                var Resend = await UserWorkation.InstaApi.RequestVerifyCodeToSMSForChallengeRequireAsync();
                canSendAgain = false;
                Sec = 60;
                newTimer.Start();
            }
            else
            {
                Statico.Notifer.Show(string.Format("Yoy should wait {0} Seconds for request code again.",Sec), Statico.NotifDelay);
            }
        }

        private async void btnok_Click(object sender, RoutedEventArgs e)
        {

            if (tbcode.Text.Length == 6 || int.TryParse(tbcode.Text, out int n))
            {
               var result = await UserWorkation.ToFactory( tbcode.Text);
                if (result.Succeeded)
                {
                    Frame.Navigate(typeof(HomePage));
                }
                else if(result.Info.ResponseType == ResponseType.ChallengeRequired)
                {
                    Frame.Navigate(typeof(ChallengePage));
                }
                else if(result.Info.ResponseType == ResponseType.CheckPointRequired)
                {
                    Statico.Notifer.Show(result.Info.Message, Statico.NotifDelay);
                }
                else
                {
                    Statico.Notifer.Show(result.Info.Message, Statico.NotifDelay);
                }
            }
            else
            {
                Statico.Notifer.Show("Please check your input.", Statico.NotifDelay);
            }

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = (string[])e.Parameter;
            username = parameters[0];
            password = parameters[1];
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            newTimer.Interval = TimeSpan.FromSeconds(1);
            newTimer.Tick += OnTimerTick;

            var TwoFactor = await UserWorkation.InstaApi.GetTwoFactorInfoAsync();
            if (TwoFactor.Succeeded)
            {
               // tbInfo.Text ="We Send Vertification code to the phone number that ended with " +TwoFactor.Value.ObfuscatedPhoneNumber;
                Sec = 60;
                newTimer.Start();
            }
        }

        private void OnTimerTick(object sender, object e)
        {
            Sec--;
            if (Sec == 0)
            {
                canSendAgain = true;
                newTimer.Stop();
            }
        }

    }
}
