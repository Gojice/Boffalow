﻿using Microsoft.Toolkit.Uwp.UI.Controls;
using Pixo.Models;
using Prism.Windows.Mvvm;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Pixo.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UnFollowPage : SessionStateAwarePage
    {

        public static TextBlock tbMax;
        public static TextBox tbNumber;
        public static Button btnUnf;
        public static ComboBox cbMet;
        public static ListView lv;
        public static InAppNotification notifers;
        public static ProgressRing pr;

        public UnFollowPage()
        {
            this.InitializeComponent();
        }

        private void tbNumberOfunFollower_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnProcess_Click(object sender, RoutedEventArgs e)
        {
            if(tbNumberOfunFollower.Text.Length ==0 && cbMethoud.SelectedIndex != 3)
            {
                Notinfer.Show("You should set the Maximum number of unfollow!",2000);
                return;
            }
            int parsedValue;
            if (!int.TryParse(tbNumberOfunFollower.Text, out parsedValue) && cbMethoud.SelectedIndex != 3)
            {
                Notinfer.Show("Please insert iniger in max number field.", 2000);
                return;
            }

            if(HistoryModel.IsStopUn)
            {
                HistoryModel.UnFirst = true;
                HistoryModel.Max = tbNumberOfunFollower.Text;
                HistoryModel.IsStopUn = !HistoryModel.IsStopUn;
                btnProcess.Content = "Stop";
                tbNumberOfunFollower.IsEnabled = false;
                cbMethoud.IsEnabled = false;
                switch (cbMethoud.SelectedIndex)
                {
                    case 0:
                        {
                            HistoryModel.Method = 0;
                            UnfollowThatNotFollow();
                        }
                        break;
                    case 1:
                        {
                            HistoryModel.Method = 1;
                            UnfollowThatFollow();
                        }
                        break;
                    case 2:
                        {
                            HistoryModel.Method = 2;
                            UnfollowAll();
                        }
                        break;
                    case 3:
                        {
                            HistoryModel.Method = 3;
                            UnFollowSelected();
                        }
                        break;
                }
                HomePage.par.IsActive = true;
            }
            else
            {
                HistoryModel.IsStopUn = true;
                btnProcess.Content = "Waiting...";
                btnProcess.IsEnabled = false;
            }
        }

        private void UnFollowSelected()
        {
            var Selected =  lvList.SelectedItems;
            if (Selected.Count == 0)
            {
                Statico.Notifer.Show(string.Format("You Should select a item for Unfollow."), Statico.NotifDelay);
                btnProcess.Content = "Process";
                tbNumberOfunFollower.IsEnabled = true;
                cbMethoud.IsEnabled = true;
                btnProcess.IsEnabled = true;
                return;
            }

            UnFollower un = new UnFollower();
            un.Selected(Selected);
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
            await UserWorkation._loadSessionDelay();
            tbMax = tbMaxNumber;
            tbNumber = tbNumberOfunFollower;
            btnUnf = btnProcess;
            cbMet = cbMethoud;
            lv = lvList;
            notifers = Notinfer;
            pr = prLoadData;

            if (HistoryModel.UnFirst)
            {
                tbNumberOfunFollower.Text = HistoryModel.Max;
                btnProcess.IsEnabled = true;
                tbNumberOfunFollower.IsEnabled = false;
                btnProcess.Content = "Stop";
                switch (HistoryModel.Method)
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
                    case 3:
                        {
                            cbMethoud.SelectedIndex = 3;
                            tbMaxNumber.Text = FollowerAnalyser.Diffrent.Count().ToString();
                            tbNumberOfunFollower.PlaceholderText = FollowerAnalyser.Diffrent.Count().ToString();
                            lvList.ItemsSource = FollowerAnalyser.Diffrent;
                        }
                        break;
                }
                HomePage.par.IsActive = true;
            }
            else
            {
                if(!FollowerAnalyser.Diffrents)
                pr.IsActive = true;
                HistoryModel.IsStopUn = true;
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
            try
            {
                tbNumberOfunFollower.IsEnabled = true;
            }
            catch { }
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
                        tbMaxNumber.Text = FollowerAnalyser.Diffrent.Count + "";
                        tbNumberOfunFollower.PlaceholderText = FollowerAnalyser.Diffrent.Count().ToString();
                        lvList.ItemsSource = FollowerAnalyser.Diffrent;
                        lvList.SelectionMode = ListViewSelectionMode.Multiple;
                        tbNumberOfunFollower.IsEnabled = false;
                        tbMaxNumber.Text = "0";
                    }
                    break;
            }
        }
        
        private void lvList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbMaxNumber.Text = lvList.SelectedItems.Count+"";
        }
    }
}
