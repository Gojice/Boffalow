using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
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
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
            Loaded += Login_Loaded;
        }

        private async void Login_Loaded(object sender, RoutedEventArgs e)
        {
            var currentView = SystemNavigationManager.GetForCurrentView();
            currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;

            var abc = await UserWorkation.LoadSession();
            if (abc)
            {
                tbUsername.Text = UserWorkation.User;
                tbPassword.Password = UserWorkation.Pass;
            }
            //tbUsername.Text = "kajokoleha";
            //tbPassword.Password = "Rmt4006nj";
            System.Diagnostics.Debug.WriteLine(Windows.Storage.ApplicationData.Current.LocalFolder.Path);
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = tbUsername.Text;
            string password = tbPassword.Password;
            if (username.Length >= 3 && password.Length >= 5)
            {
                btnLogin.IsEnabled = false;
                tbPassword.IsEnabled = false;
                tbUsername.IsEnabled = false;
                //in code ejra mishe?

                var result = await UserWorkation.Login(username, password);

                HandleLogin(result);

                btnLogin.IsEnabled = true;
                tbPassword.IsEnabled = true;
                tbUsername.IsEnabled = true;
            }
        }

        private async void HandleLogin(IResult<InstaLoginResult> result)
        {
            if (result.Succeeded)
            {
                Frame.Navigate(typeof(HomePage));
            }
            else if (result.Value == InstaLoginResult.TwoFactorRequired)
            {
                string[] userinfo = { tbUsername.Text, tbPassword.Password };
                Frame.Navigate(typeof(TowFactorPage), userinfo);
            }
            else if (result.Value == InstaLoginResult.ChallengeRequired)
            {
                IResult<InstaChallengeRequireVerifyMethod> challenge = await UserWorkation.InstaApi.GetChallengeRequireVerifyMethodAsync();
                if (challenge.Succeeded)
                {
                    if (challenge.Value.SubmitPhoneRequired)
                    {
                        Frame.Navigate(typeof(SubmitPhonePage));
                    }
                    else
                    {
                        Statico.Challenge = challenge;
                        Frame.Navigate(typeof(ChallengeInfoPage));
                    }

                }
                else if (result.Value == InstaLoginResult.BadPassword)
                {
                    Statico.Notifer.Show("The password is incorrect.", Statico.NotifDelay);
                }
                else if (result.Value == InstaLoginResult.InvalidUser)
                {
                    Statico.Notifer.Show("Username is incorrect.", Statico.NotifDelay);
                }
                else if (result.Value == InstaLoginResult.InactiveUser)
                {
                    Statico.Notifer.Show(result.Info.Message + ".", Statico.NotifDelay);
                }
                else if (result.Value == InstaLoginResult.LimitError)
                {
                    Statico.Notifer.Show(result.Info.Message + ".", Statico.NotifDelay);
                }
                else if (result.Value == InstaLoginResult.Exception)
                {
                    Statico.Notifer.Show(result.Info.Message + ".", Statico.NotifDelay);
                }

                //in code ejra mishe?
            }

        }

        private void tbForget_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(ForgetPage));
        }
    }
}
