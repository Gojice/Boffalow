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
    public sealed partial class SubmitPhonePage : SessionStateAwarePage
    {
        public SubmitPhonePage()
        {
            this.InitializeComponent();
        }

        private async void btnok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSubmitPhoneForChallenge.Text) ||
                     string.IsNullOrWhiteSpace(txtSubmitPhoneForChallenge.Text))
                {
                    Statico.Notifer.Show("Please type a valid phone number(with country code).", Statico.NotifDelay);
                    return;
                }
                var phoneNumber = txtSubmitPhoneForChallenge.Text;
                if (!phoneNumber.StartsWith("+"))
                    phoneNumber = $"+{phoneNumber}";

                var submitPhone = await UserWorkation.InstaApi.SubmitPhoneNumberForChallengeRequireAsync(phoneNumber);
                if (submitPhone.Succeeded)
                {
                    Frame.Navigate(typeof(VerifyPage));
                }
                else
                    Statico.Notifer.Show(submitPhone.Info.Message, Statico.NotifDelay);
            }
            catch (Exception ex) { Statico.Notifer.Show(ex.Message, Statico.NotifDelay); }
        }
    }
}
