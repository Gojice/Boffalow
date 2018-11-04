using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using InstagramApiSharp.Classes;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Toolkit.Uwp.UI.Animations;
using Microsoft.Toolkit.Uwp.UI;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading.Tasks;
using InstagramApiSharp;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Pixo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public static TextBlock DiffrentNumber;
        public static TextBlock FollowerNumber;
        public static TextBlock FollowingNumber;
        public static TextBlock FansNumber;
        public static ProgressRing par;

        public HomePage()
        {
            this.InitializeComponent();
        }
        // Define max memory cache size
        


        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DiffrentNumber = tbDiffrentNumber;
            FollowerNumber = tbFollowerNumber;
            FollowingNumber = tbFollowingNumber;
            FansNumber = tbFansNumber;
            par = prLoadData;
            frmHome.Navigate(typeof(UnFollowPage));
            //  await grdHome.Blur(7, 500, 0).StartAsync();
            var ok = await UserWorkation.LoadSession();
            if (ok)
            {
                Filer filer = new Filer();
                var json = await filer.ReadSession();

                ImageCache.Instance.MaxMemoryCacheCount = 200;

                var S = JsonConvert.DeserializeObject<SeissonClass>(json);
                ppUser.DisplayName = UserWorkation.User;
                tbUserName.Text = UserWorkation.User;

                //Precache data and save it in on local hard drive
                try
                {
                    await ImageCache.Instance.PreCacheAsync(new Uri(S.UserSession.LoggedInUser.ProfilePicture), true, false);
                    var bitmapImage = await ImageCache.Instance.GetFromCacheAsync(new Uri(S.UserSession.LoggedInUser.ProfilePicture));
                    ppUser.ProfilePicture = bitmapImage;
                }
                catch(Exception ex)
                {
                    Debug.WriteLine("Avatar image Caching problem: "+ex);
                }

                FollowerAnalyser.AllFollower();
                FollowerAnalyser.AllFollowing();

                while (FollowerAnalyser.AllFollowers != true ||  FollowerAnalyser.AllFollowings != true)
                {
                    await Task.Delay(100);
                }

                prLoadData.IsActive = false;
                tbFollowerNumber.Text = FollowerAnalyser.Followers.Count().ToString();
                tbFollowingNumber.Text = FollowerAnalyser.Followings.Count().ToString();

                FollowerAnalyser.WhiteList();

                FollowerAnalyser.Diffrensial();
                while (!FollowerAnalyser.Diffrents)
                {
                    await Task.Delay(100);
                }

                tbDiffrentNumber.Text = FollowerAnalyser.Diffrent.Count().ToString();
                tbFansNumber.Text = FollowerAnalyser.Fans.Count().ToString();
            }
        }

        private void btnAutoFollow_Click(object sender, RoutedEventArgs e)
        {
            frmHome.Navigate(typeof(AutoFollowPage));
        }

        private void btnAutoUnFollow_Click(object sender, RoutedEventArgs e)
        {
            frmHome.Navigate(typeof(UnFollowPage));
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            frmHome.Navigate(typeof(SettingPage));
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            frmHome.Navigate(typeof(TestPage));
        }
    }
}
