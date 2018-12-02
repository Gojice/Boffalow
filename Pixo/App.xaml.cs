using Pixo.Extensions;
using Prism.Unity.Windows;
using System.Threading.Tasks;
using Unity;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Pixo
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : PrismUnityApplication
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        protected override UIElement CreateShell(Frame rootFrame)
        {
            var shell = Container.Resolve<AppShell>();
            shell.SetContentFrame(rootFrame);
            return shell;
        }

        /// <summary>
        /// Logic of app initialization.
        /// This is the best place to register the services in Unity container.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            #region Services

            #endregion

            //Container.RegisterInstance<IResourceLoader>(new ResourceLoaderAdapter(new ResourceLoader()));
            Container.RegisterInstance(SessionStateService);
            Container.RegisterInstance(NavigationService);

            return base.OnInitializeAsync(args);
        }

        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

            if (localSettings.Values["ihaveaaccount"] is string acc && acc == "yes")
                NavigationService.Navigate(PageTokens.Home);

            else
                NavigationService.Navigate(PageTokens.Login);

            ExtendAcrylicIntoTitleBar();
            return Task.FromResult(true); //This is a little trick because this method returns a Task.
        }

        /// Extend acrylic into the title bar. 
        private void ExtendAcrylicIntoTitleBar()
        {
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            titleBar.ForegroundColor = Colors.White;
            titleBar.ButtonForegroundColor = Colors.White;
        }
    }
}
