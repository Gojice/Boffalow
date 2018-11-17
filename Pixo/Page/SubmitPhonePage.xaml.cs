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

namespace Pixo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SubmitPhonePage : Page
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
