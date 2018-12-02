using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Toolkit.Uwp.UI;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Windows.UI.Core;
using Pixo.Models;
using Prism.Windows.Mvvm;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Pixo.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : SessionStateAwarePage
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

        string page = "AutoUnFollow";

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DiffrentNumber = tbDiffrentNumber;
            FollowerNumber = tbFollowerNumber;
            FollowingNumber = tbFollowingNumber;
            FansNumber = tbFansNumber;
            par = prLoadData;
            frmHome.Navigate(typeof(UnFollowPage));
            var ok = await UserWorkation.LoadSession();
            if (ok)
            {
                
                ppUser.DisplayName = UserWorkation.User;
                tbUserName.Text = UserWorkation.User;

                Statico.Notifer.Show("Please Wait... We try to get and analyse your data.", Statico.NotifDelay);
                var requests = new List<Task>();
                requests.Add(Task.Factory.StartNew(new Action (LoadAvatar)));
                requests.Add(Task.Factory.StartNew(new Action(FollowerAnalyser.AllFollowing)));
                requests.Add(Task.Factory.StartNew(new Action(FollowerAnalyser.AllFollower)));

                Task.WaitAll(requests.ToArray());


                while (FollowerAnalyser.AllFollowers != true ||  FollowerAnalyser.AllFollowings != true)
                {
                    await Task.Delay(100);
                }

                UnFollowPage.pr.IsActive = false;

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

        private async void LoadAvatar()
        {
            Filer filer = new Filer();
            var json = await filer.ReadSession();

            ImageCache.Instance.MaxMemoryCacheCount = 200;

            var S = JsonConvert.DeserializeObject<SessionModel>(json);

            //Precache data and save it in on local hard drive
            try
            {
                await ImageCache.Instance.PreCacheAsync(new Uri(S.UserSession.User.ProfilePicture), true, false);
                var bitmapImage = await ImageCache.Instance.GetFromCacheAsync(new Uri(S.UserSession.User.ProfilePicture));
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                    ppUser.ProfilePicture = bitmapImage;
                });
            }
            catch (Exception ex)
            {
                var X = await UserWorkation.InstaApi.GetCurrentUserAsync();
                await ImageCache.Instance.PreCacheAsync(new Uri(X.Value.HdProfilePicture.Uri), true, false);
                var bitmapImage = await ImageCache.Instance.GetFromCacheAsync(new Uri(X.Value.HdProfilePicture.Uri));
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                    ppUser.ProfilePicture = bitmapImage;
                });
            }
            finally
            {
                var X = await UserWorkation.InstaApi.GetCurrentUserAsync();
                await ImageCache.Instance.PreCacheAsync(new Uri(X.Value.HdProfilePicture.Uri), true, false);
                var bitmapImage = await ImageCache.Instance.GetFromCacheAsync(new Uri(X.Value.HdProfilePicture.Uri));
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,() =>{
                    ppUser.ProfilePicture = bitmapImage;
                });
               
                
            }
        }
        
        private void btnAutoFollow_Click(object sender, RoutedEventArgs e)
        {
            if (page != "AutoFollowPage")
            {
                page = "AutoFollowPage";
                frmHome.Navigate(typeof(AutoFollowPage));
            }
        }

        private void btnAutoUnFollow_Click(object sender, RoutedEventArgs e)
        {
            if (page != "AutoUnFollow")
            {
                page = "AutoUnFollow";
                frmHome.Navigate(typeof(UnFollowPage));
            }
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            if (page != "SettingPage")
            {
                page = "SettingPage";
                frmHome.Navigate(typeof(SettingPage));
            }
        }

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            if (page != "AboutPage")
            {
                page = "AboutPage";
                frmHome.Navigate(typeof(AboutPage));
            }
        }
    }
}
