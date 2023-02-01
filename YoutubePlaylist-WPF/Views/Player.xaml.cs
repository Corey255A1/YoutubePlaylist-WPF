using Microsoft.Web.WebView2.Core;
using System.Windows.Controls;

namespace YoutubePlaylistWPF.Views
{
    /// <summary>
    /// Interaction logic for Player.xaml
    /// </summary>
    public partial class Player : UserControl
    {
        public Player()
        {
            InitializeComponent();
            Loaded += PlayerLoaded;
        }

        private async void PlayerLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await webView.EnsureCoreWebView2Async();
            webView.NavigateToString("<html><head><style>html,body{background:lightgray;}</style><heaed><body><h1>Hello World</h1></body></html>");
        }
    }
}
