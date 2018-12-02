using Prism.Windows.Mvvm;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Pixo.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ChallengePage : SessionStateAwarePage
    {
        public ChallengePage()
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
