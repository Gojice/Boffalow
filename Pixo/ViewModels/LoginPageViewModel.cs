using System.Collections.Generic;
using System.Windows.Input;
using InstagramApiSharp.Classes;
using Pixo.Extensions;
using Prism.Commands;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;

namespace Pixo.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        #region Fields

        private readonly INavigationService _navigationService;
        private bool _isFormEnabled = true;

        #endregion

        #region Properties

        public string Username { get; set; }

        public string Password { get; set; }

        public bool IsFormEnabled
        {
            get => _isFormEnabled;
            set => SetProperty(ref _isFormEnabled, value);
        }

        public ICommand LoginCommand { get; private set; }

        public ICommand ForgotCommand
        {
            get => new DelegateCommand(() => _navigationService.Navigate(PageTokens.Forget));
        }

        #endregion

        #region Ctor

        public LoginPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            LoginCommand = new DelegateCommand(ExecuteLoginCommand);
        }

        #endregion

        #region Methods

        private async void ExecuteLoginCommand()
        {
            if (Username.Length >= 3)
            {
                IsFormEnabled = false;

                var result = await UserWorkation.Login(Username, Password);
                HandleLogin(result);

                IsFormEnabled = true;
            }
            else
            {
                Statico.Notifer.Show("Username should be at least 3 character or more.", Statico.NotifDelay);
            }
        }

        private async void HandleLogin(IResult<InstaLoginResult> result)
        {
            string errMsg = string.Empty;

            switch (result.Value)
            {
                case InstaLoginResult.Success:
                    _navigationService.Navigate(PageTokens.Home);
                    break;

                case InstaLoginResult.TwoFactorRequired:
                    _navigationService.Navigate(PageTokens.TwoFactor, new string[] { Username, Password });
                    break;

                case InstaLoginResult.ChallengeRequired:
                    IResult<InstaChallengeRequireVerifyMethod> challenge = await UserWorkation.InstaApi.GetChallengeRequireVerifyMethodAsync();

                    if (challenge.Succeeded)
                    {
                        if (challenge.Value.SubmitPhoneRequired)
                        {
                            _navigationService.Navigate(PageTokens.SubmitPhone);
                        }
                        else
                        {
                            Statico.Challenge = challenge;
                            _navigationService.Navigate(PageTokens.ChallengeInfo);
                        }
                    }
                    else
                        errMsg = "Can't retrieve challenge. Pleas try again.";
                    break;

                case InstaLoginResult.BadPassword:
                    errMsg = "The password is incorrect.";
                    break;

                case InstaLoginResult.InvalidUser:
                    errMsg = "Username is incorrect.";
                    break;

                case InstaLoginResult.InactiveUser:
                case InstaLoginResult.LimitError:
                case InstaLoginResult.Exception:
                    errMsg = result.Info.Message + ".";
                    break;
            }

            if (!string.IsNullOrEmpty(errMsg))
                Statico.Notifer.Show(errMsg, Statico.NotifDelay);
        }

        public async override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            var abc = await UserWorkation.LoadSession();
            if (abc)
            {
                Username = UserWorkation.User;
                Password = UserWorkation.Pass;
            }

            base.OnNavigatedTo(e, viewModelState);
        }

        #endregion
    }
}
