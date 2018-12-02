using Prism.Windows.Navigation;

namespace Pixo.Extensions
{
    public static class NavigationExtension
    {
        public static void Navigate(this INavigationService navigationService, PageTokens token)
        {
            navigationService.Navigate(token, null);
        }

        public static void Navigate(this INavigationService navigationService, PageTokens token, object param)
        {
            navigationService.Navigate(token.ToString(), param);
        }
    }
}
