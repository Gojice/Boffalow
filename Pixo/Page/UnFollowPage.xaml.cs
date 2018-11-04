using InstagramApiSharp.Classes;
using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Pixo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UnFollowPage : Page
    {

        public static TextBlock tbMax;
        public static TextBox tbNumber;
        public static Button btnUnf;
        public static ComboBox cbMet;
        public static ListView lv;
        public static InAppNotification notifers;


        public UnFollowPage()
        {
            this.InitializeComponent();
        }

        private void tbNumberOfunFollower_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnProcess_Click(object sender, RoutedEventArgs e)
        {
            if(tbNumberOfunFollower.Text.Length ==0)
            {
                Notinfer.Show("You should set the Maximum number of unfollow!",2000);
                return;
            }
            int parsedValue;
            if (!int.TryParse(tbNumberOfunFollower.Text, out parsedValue))
            {
                Notinfer.Show("Please insert iniger in max number field.", 2000);
                return;
            }

            if(History.isStopUn)
            {
                History.unFirst = true;
                History.Max = tbNumberOfunFollower.Text;
                History.isStopUn = !History.isStopUn;
                btnProcess.Content = "Stop";
                tbNumberOfunFollower.IsEnabled = false;
                cbMethoud.IsEnabled = false;
                switch (cbMethoud.SelectedIndex)
                {
                    case 0:
                        {
                            History.Methud = 0;
                            UnfollowThatNotFollow();
                        }
                        break;
                    case 1:
                        {
                            History.Methud = 1;
                            UnfollowThatFollow();
                        }
                        break;
                    case 2:
                        {
                            History.Methud = 2;
                            UnfollowAll();
                        }
                        break;
                    case 3:
                        {
                            History.Methud = 2;
                            UnfollowAll();
                        }
                        break;
                }
                HomePage.par.IsActive = true;
            }
            else
            {
                History.isStopUn = true;
                btnProcess.Content = "Waiting...";
                btnProcess.IsEnabled = false;
            }
        }

        private void UnfollowAll()
        {
            if (Convert.ToInt32(tbNumberOfunFollower.Text) > Convert.ToInt32(tbMaxNumber.Text))
            {
                MessageDialog message = new MessageDialog("The Number of Unfollow should be less than Max Number.");
                btnProcess.Content = "Process";
                tbNumberOfunFollower.IsEnabled = true;
                cbMethoud.IsEnabled = true;
                btnProcess.IsEnabled = true;
                return;
            }
            UnFollower un = new UnFollower();
            un.All(Convert.ToInt32(tbNumberOfunFollower.Text));
        }

        private void UnfollowThatFollow()
        {
            if (Convert.ToInt32(tbNumberOfunFollower.Text) > Convert.ToInt32(tbMaxNumber.Text))
            {
                MessageDialog message = new MessageDialog("The Number of Unfollow should be less than Max Number.");
                btnProcess.Content = "Process";
                tbNumberOfunFollower.IsEnabled = true;
                cbMethoud.IsEnabled = true;
                btnProcess.IsEnabled = true;
                return;
            }

            UnFollower un = new UnFollower();
            un.FollowerThatFollowingMe(Convert.ToInt32(tbNumberOfunFollower.Text));
        }

        private void UnfollowThatNotFollow()
        {
            if(Convert.ToInt32(tbNumberOfunFollower.Text)> Convert.ToInt32(tbMaxNumber.Text))
            {
                Notinfer.Show("The Number of Unfollow should be less than Max Number.",2000);
                btnProcess.Content = "Process";
                tbNumberOfunFollower.IsEnabled = true;
                cbMethoud.IsEnabled = true;
                btnProcess.IsEnabled = true;
                return;
            }

            UnFollower un = new UnFollower();
            un.NotFollowingMe(Convert.ToInt32(tbNumberOfunFollower.Text));
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            tbMax = tbMaxNumber;
            tbNumber = tbNumberOfunFollower;
            btnUnf = btnProcess;
            cbMet = cbMethoud;
            lv = lvList;
            notifers = Notinfer;

            if (History.unFirst)
            {
                tbNumberOfunFollower.Text = History.Max;
                btnProcess.IsEnabled = true;
                tbNumberOfunFollower.IsEnabled = false;
                btnProcess.Content = "Stop";
                switch (History.Methud)
                {
                    case 0:
                        {
                            cbMethoud.SelectedIndex = 0;
                            tbMaxNumber.Text = FollowerAnalyser.Diffrent.Count().ToString();
                            tbNumberOfunFollower.PlaceholderText = FollowerAnalyser.Diffrent.Count().ToString();
                            lvList.ItemsSource = FollowerAnalyser.Diffrent;
                        }
                        break;
                    case 1:
                        {
                            cbMethoud.SelectedIndex = 1;
                            tbMaxNumber.Text= FollowerAnalyser.Equals.Count().ToString();
                            tbNumberOfunFollower.PlaceholderText = FollowerAnalyser.Equals.Count().ToString();
                            lvList.ItemsSource = FollowerAnalyser.Equals;
                        }
                        break;
                    case 2:
                        {
                            cbMethoud.SelectedIndex = 2;
                            tbMaxNumber.Text = FollowerAnalyser.Followers.Count().ToString();
                            tbNumberOfunFollower.PlaceholderText = FollowerAnalyser.Followers.Count().ToString();
                            lvList.ItemsSource = FollowerAnalyser.Followers;
                        }
                        break;
                }
                HomePage.par.IsActive = true;
            }
            else
            {
                History.isStopUn = true;
                while (!FollowerAnalyser.Diffrents)
                {
                    await Task.Delay(100);
                }
                cbMethoud.IsEnabled = true;
                btnProcess.IsEnabled = true;
                tbMaxNumber.Text = FollowerAnalyser.Diffrent.Count + "";
                tbNumberOfunFollower.PlaceholderText = FollowerAnalyser.Diffrent.Count().ToString();
                lvList.ItemsSource = FollowerAnalyser.Diffrent;

            }
        }

        private async void cbMethoud_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            while (!FollowerAnalyser.Diffrents)
            {
                await Task.Delay(100);
            }
            while (FollowerAnalyser.AllFollowers != true || FollowerAnalyser.AllFollowings != true)
            {
                await Task.Delay(100);
            }

            switch (cbMethoud.SelectedIndex)
            {
                case 0:
                    {
                        try
                        {
                        tbMaxNumber.Text = FollowerAnalyser.Diffrent.Count+"";
                        tbNumberOfunFollower.PlaceholderText = FollowerAnalyser.Diffrent.Count().ToString();
                        lvList.ItemsSource = FollowerAnalyser.Diffrent;
                            lvList.SelectionMode = ListViewSelectionMode.None;
                        }
                        catch { }
                    }
                    break;
                case 1:
                    {
                        tbNumberOfunFollower.PlaceholderText = FollowerAnalyser.Equals.Count().ToString();
                        tbMaxNumber.Text = FollowerAnalyser.Equals.Count.ToString();
                        lvList.ItemsSource = FollowerAnalyser.Equals;
                        lvList.SelectionMode = ListViewSelectionMode.None;
                    }
                    break;
                case 2:
                    {
                        tbNumberOfunFollower.PlaceholderText = FollowerAnalyser.Followings.Count().ToString();
                        tbMaxNumber.Text = FollowerAnalyser.Followings.Count.ToString();
                        lvList.ItemsSource = FollowerAnalyser.Followings;
                        lvList.SelectionMode = ListViewSelectionMode.None;
                    }
                    break;
                case 3:
                    {
                        tbNumberOfunFollower.PlaceholderText = FollowerAnalyser.Followings.Count().ToString();
                        tbMaxNumber.Text = FollowerAnalyser.Followings.Count.ToString();
                        lvList.ItemsSource = FollowerAnalyser.Followings;
                        lvList.SelectionMode = ListViewSelectionMode.Multiple;
                    }
                    break;
            }
        }
    }
}
