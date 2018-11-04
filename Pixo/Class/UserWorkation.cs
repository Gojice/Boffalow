using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Logger;
using Windows.Storage;

namespace Pixo
{
    static class UserWorkation
    {
        internal static string User { get { return _user; } }
        internal static string Pass { get { return _pass; } }

       static  string _lastUser;
        private static string _user;
        private static string _pass;
        private static string _code;
        // khob in az in, dg?
        // bego
        //run kon



        internal static IInstaApi InstaApi;
        private static Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        //login:
        public static async Task<IResult<InstaLoginResult>> Login(string username, string password)
        {
            _user = username;
            _pass = password;

            return await _login();
        }

        //two Factory login:
        public static async Task<IResult<InstaLoginTwoFactorResult>> ToFactory(string Code)
        {
            _code = Code;

            return await _ToFactory();
        }

        private static async Task<IResult<InstaLoginTwoFactorResult>> _ToFactory()
        {
            // in dige ch sighe eie:|
            IResult<InstaLoginTwoFactorResult> twoFactorLogin = await InstaApi.TwoFactorLoginAsync(_code);
            return twoFactorLogin;
        }

        private static async Task<IResult<InstaLoginResult>> _login()
        {
            var userSession = new UserSessionData
            {
                UserName = _user,
                Password = _pass
            };

            InstaApi = InstaApiBuilder.CreateBuilder()
                .SetUser(userSession)
                .Build();
            // vpnet ro bband

             var logInResult = await InstaApi.LoginAsync();
            Debug.WriteLine(logInResult.Succeeded);

                SaveSession();
                return logInResult;

        }

        //save sessin to file:
        private static async void SaveSession()
        {
            if (InstaApi == null)
                return;
            if (!InstaApi.IsUserAuthenticated)
                return;

            _lastUser = _user;
            // Create file; replace if exists.
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            // mage mikhai chand ta id azash login koni hamzaman? are
            // bish az 5 ta bashe block mishi, dah ta 1a dare
            // dah ta yeka dare yani che:| 10 ta ja dare, bedone moshkel

            Windows.Storage.StorageFile File = await storageFolder.CreateFileAsync(_user + ".txt", Windows.Storage.CreationCollisionOption.ReplaceExisting);
            await Windows.Storage.FileIO.WriteTextAsync(File, InstaApi.GetStateDataAsString());
            localSettings.Values["ihaveaaccount"] = "yes";
            localSettings.Values["LastUser"] = _lastUser;
        }


        public static async Task<bool> LoadSession()
        {
            return await _loadSession();
        }
        private static async Task<bool> _loadSession()
        {
            try
            {
                var us = localSettings.Values["LastUser"] as string;
                if (string.IsNullOrEmpty(us))
                    return false;
                Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

                Windows.Storage.StorageFile File = await storageFolder.GetFileAsync(us + ".txt");
                Debug.WriteLine(storageFolder.Path);
                var json =   await Windows.Storage.FileIO.ReadTextAsync(File);

                Debug.WriteLine(json);
                var userSession = new UserSessionData
                {
                    // ina ro nemikhad avaz koni, alaki faghat behesh midim ke tuye code badi error nade
                    UserName = "USER",
                    Password = "PASS"
                };

                // yani inja
                InstaApi = InstaApiBuilder.CreateBuilder()
                    .SetUser(userSession)
                    //.UseLogger(new DebugLogger(LogLevel.Exceptions))
                    .SetRequestDelay(RequestDelay.FromSeconds(5, 15))
                    .Build();

                InstaApi.LoadStateDataFromString(json);

                _user = _lastUser = InstaApi.GetLoggedUser().UserName;
                _pass = InstaApi.GetLoggedUser().Password;
                Debug.WriteLine($"{_user} logged in successfully. passesh ham ine: {_pass}");
                if (InstaApi.IsUserAuthenticated)
                    return true;
            }
            catch { }

            return false;
        }



        public static async void SetDelay(int delay)
        {
            try
            {
                var us = localSettings.Values["LastUser"] as string;

                Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

                Windows.Storage.StorageFile File = await storageFolder.GetFileAsync(us + ".txt");
                Debug.WriteLine(storageFolder.Path);
                var json = await Windows.Storage.FileIO.ReadTextAsync(File);

                Debug.WriteLine(json);
                var userSession = new UserSessionData
                {
                    // ina ro nemikhad avaz koni, alaki faghat behesh midim ke tuye code badi error nade
                    UserName = "USER",
                    Password = "PASS"
                };

                // yani inja
                InstaApi = InstaApiBuilder.CreateBuilder()
                    .SetUser(userSession)
                    //.UseLogger(new DebugLogger(LogLevel.Exceptions))
                    .SetRequestDelay(RequestDelay.FromSeconds(5, delay))
                    .Build();
            }
            catch
            {

            }
        }

        public static async Task<bool> Logout()
        {
            var logout = await InstaApi.LogoutAsync();
            if (logout.Succeeded)
            {
                localSettings.Values["ihaveaaccount"] = "no";
                localSettings.Values["LastUser"] = "";
                Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                StorageFile manifestFile = await storageFolder.GetFileAsync(_user + ".txt");
                await manifestFile.DeleteAsync();

            }
            return logout.Succeeded;
        }
    }
}
