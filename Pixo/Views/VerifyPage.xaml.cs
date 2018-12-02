using InstagramApiSharp.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
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
    public sealed partial class VerifyPage : Page
    {

        bool canSendAgain = false;
        int Sec = 60;
        DispatcherTimer newTimer = new DispatcherTimer();

        public VerifyPage()
        {
            this.InitializeComponent();
        }

        private async void btnok_Click(object sender, RoutedEventArgs e)
        {
            tbVerifyCode.Text = tbVerifyCode.Text.Trim();
            tbVerifyCode.Text = tbVerifyCode.Text.Replace(" ", "");
            var regex = new Regex(@"^-*[0-9,\.]+$");
            if (!regex.IsMatch(tbVerifyCode.Text))
            {
                Statico.Notifer.Show("Verification code is numeric!!!",Statico.NotifDelay);
                return;
            }
            if (tbVerifyCode.Text.Length != 6)
            {
                Statico.Notifer.Show("erification code must be 6 digits!!!", Statico.NotifDelay);
                return;
            }
            try
            {
                var verifyLogin = await UserWorkation.InstaApi.VerifyCodeForChallengeRequireAsync(tbVerifyCode.Text);
                if (verifyLogin.Succeeded)
                {
                    UserWorkation.SaveSession();
                    Frame.Navigate(typeof(HomePage));
                }
                else
                {
                    // two factor is required
                    if (verifyLogin.Value == InstaLoginResult.TwoFactorRequired)
                    {
                        Frame.Navigate(typeof(TowFactorPage));
                    }
                    else
                        Statico.Notifer.Show(verifyLogin.Info.Message, Statico.NotifDelay);
                }
            }
            catch (Exception ex)
            {
                Statico.Notifer.Show(ex.Message, Statico.NotifDelay);
            }
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
                Statico.Notifer.Show(string.Format("Yoy should wait {0} Seconds for request code again.", Sec), Statico.NotifDelay);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            newTimer.Interval = TimeSpan.FromSeconds(1);
            newTimer.Tick += OnTimerTick;
            Sec = 60;
            newTimer.Start();
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
