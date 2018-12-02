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
    public sealed partial class ChallengeInfoPage : Page
    {
        public ChallengeInfoPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var challenge = Statico.Challenge;

            if (challenge.Value.StepData != null)
            {
                if (!string.IsNullOrEmpty(challenge.Value.StepData.PhoneNumber))
                {
                    rbPhone.Content = challenge.Value.StepData.PhoneNumber;
                    rbPhone.Visibility = Visibility.Visible;
                }
                if (!string.IsNullOrEmpty(challenge.Value.StepData.Email))
                {
                    rbEmail.Content = challenge.Value.StepData.Email;
                    rbEmail.Visibility = Visibility.Visible;
                }
            }
        }

        private async void btnok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (rbPhone.IsChecked == true)
                {
                    var phone = await UserWorkation.InstaApi.RequestVerifyCodeToSMSForChallengeRequireAsync();
                    if (phone.Succeeded)
                    {
                        Frame.Navigate(typeof(VerifyPage));
                    }
                    else
                        Statico.Notifer.Show(phone.Info.Message, Statico.NotifDelay);
                }
                else if(rbEmail.IsChecked == true)
                {
                    var email = await UserWorkation.InstaApi.RequestVerifyCodeToEmailForChallengeRequireAsync();
                    if (email.Succeeded)
                    {
                        Frame.Navigate(typeof(VerifyPage));
                    }
                    else
                        Statico.Notifer.Show(email.Info.Message, Statico.NotifDelay);
                }
            }
            catch (Exception ex)
            {
                Statico.Notifer.Show(ex.Message, Statico.NotifDelay);
            }
        }
    }
}
