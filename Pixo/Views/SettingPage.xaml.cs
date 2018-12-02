using Prism.Windows.Mvvm;
using System;
using Windows.UI.Xaml;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Pixo.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingPage : SessionStateAwarePage
    {
        private static Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public SettingPage()
        {
            this.InitializeComponent();
        }

        private async void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            var Logout = await UserWorkation.InstaApi.LogoutAsync();
            if (Logout.Succeeded)
            {
                // Delete file.
                Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                Windows.Storage.StorageFile File = await storageFolder.GetFileAsync(UserWorkation.User + ".txt");

                await File.DeleteAsync();

                localSettings.Values["ihaveaaccount"] = "no";
                localSettings.Values["LastUser"] = "";

                
                AppShell.framer.Navigate(typeof(LoginPage));
            }
            else
            {
                Notinfer.Show("Error: " + Logout.Info.Message, 2000);
            }
        }
    }
}
