using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Pixo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AppShell : Page
    {
        public static Frame framer;
        private Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public AppShell()
        {
            this.InitializeComponent();
        }

        public void SetContentFrame(Frame frame)
        {
            myFrame.Content = frame;
            Statico.Notifer = Notifer;
            framer = myFrame;
        }
    }
}
