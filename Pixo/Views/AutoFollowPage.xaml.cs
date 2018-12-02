using Prism.Windows.Mvvm;
using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Pixo.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AutoFollowPage : SessionStateAwarePage
    {

        public static ListView Lister;
        public static TextBlock tbCounter;

        public AutoFollowPage()
        {
            this.InitializeComponent();
            Lister = lvList;
            tbCounter = tbNumberFollower;
        }

        private void btnProcess_Click(object sender, RoutedEventArgs e)
        {
            
            switch (cbMethoud.SelectedIndex)
            {
                case 0:
                    {
                        Nested();
                    }
                    break;
                case 1:
                    {
                        Followerofuser();
                    }
                    break;
            }
        }




        //Methouds:

        private void Followerofuser()
        {
            AutoFollowerc followerc = new AutoFollowerc();
            int num = 0;
            if (tbNumberOfFollower.Text.Length > 0)
            {
                num = Convert.ToInt32(tbNumberOfFollower.Text);
            }
            followerc.Start(num, Convert.ToInt32(sldSpeed.Value), "Followerofuser", tbSourcer.Text);
        }


        private void Nested()
        {
            AutoFollowerc followerc = new AutoFollowerc();
            int num = 0;
            if (Uri.IsWellFormedUriString(tbSourcer.Text, UriKind.RelativeOrAbsolute))
            {
                if (tbNumberOfFollower.Text.Length > 0)
                {
                    num = Convert.ToInt32(tbNumberOfFollower.Text);
                }
                followerc.Start(num, Convert.ToInt32(sldSpeed.Value), "Nested", tbSourcer.Text);
            }
            else
            {
                Debug.WriteLine("Nested error: is not url");
            }
        }

        private void tbNumberOfFollower_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(tbNumberOfFollower.Text, "[^0-9]"))
            {
                tbNumberOfFollower.Text = tbNumberOfFollower.Text.Remove(tbNumberOfFollower.Text.Length - 1);
            }

        }
    }
}
