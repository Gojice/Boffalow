using Prism.Windows.Mvvm;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Pixo.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ChallengeInfoPage : SessionStateAwarePage
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
