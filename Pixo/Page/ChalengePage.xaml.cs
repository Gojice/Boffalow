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
    public sealed partial class ChalengePage : Page
    {
        public ChalengePage()
        {
            this.InitializeComponent();
        }

        private async void btnok_Click(object sender, RoutedEventArgs e)
        {
            var challenge = await UserWorkation.InstaApi.GetChallengeRequireVerifyMethodAsync();
            if (challenge.Succeeded)
            {
                if (challenge.Value.SubmitPhoneRequired)
                {

                }
                else
                {

                }
            }
        }
        private void tbresendCode_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }
    }
}
