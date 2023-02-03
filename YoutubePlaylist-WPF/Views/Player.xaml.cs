//Corey Wunderlich WunderVision 2023
//https://www.wundervisionenvisionthefuture.com/
using Microsoft.Web.WebView2.Core;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using YoutubePlaylistWPF.Data;

namespace YoutubePlaylistWPF.Views
{
    public partial class Player : UserControl
    {

        public string WebViewHTMLPath
        {
            get { return (string)GetValue(WebViewHTMLPathProperty); }
            set { SetValue(WebViewHTMLPathProperty, value); }
        }

        public static readonly DependencyProperty WebViewHTMLPathProperty =
            DependencyProperty.Register("WebViewHTMLPath", typeof(string), typeof(Player), new PropertyMetadata(""));



        public ICommand YoutubePlayerStatusReceived
        {
            get { return (ICommand)GetValue(YoutubePlayerStatusReceivedProperty); }
            set { SetValue(YoutubePlayerStatusReceivedProperty, value); }
        }

        public static readonly DependencyProperty YoutubePlayerStatusReceivedProperty =
            DependencyProperty.Register("YoutubePlayerStatusReceived", typeof(ICommand), typeof(Player), new PropertyMetadata(null));



        public string YoutubeVideoURL
        {
            get { return (string)GetValue(YoutubeVideoURLProperty); }
            set { SetValue(YoutubeVideoURLProperty, value); }
        }

        public static readonly DependencyProperty YoutubeVideoURLProperty =
            DependencyProperty.Register("YoutubeVideoURL", typeof(string), typeof(Player), new PropertyMetadata("", OnYoutubeVideoURLChanged));


        public Player()
        {
            InitializeComponent();
            Loaded += PlayerLoaded;
        }

        private async void PlayerLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await webView.EnsureCoreWebView2Async();
            string html = await System.IO.File.ReadAllTextAsync(WebViewHTMLPath);
            webView.NavigateToString(html);
            webView.CoreWebView2.WebMessageReceived += YoutubeWebviewWebMessageReceived;
        }

        private void YoutubeWebviewWebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            YoutubeAPIStatus? status = System.Text.Json.JsonSerializer.Deserialize<Data.YoutubeAPIStatus>(e.WebMessageAsJson);
            if (status == null) { return; }

            YoutubePlayerStatusReceived?.Execute(status);
        }

        private static void OnYoutubeVideoURLChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Player)d).PlayVideo((string)e.NewValue);
        }

        private void PlayClick(object sender, System.Windows.RoutedEventArgs e)
        {
            webView.ExecuteScriptAsync("play()");
        }

        private void PauseClick(object sender, System.Windows.RoutedEventArgs e)
        {
            webView.ExecuteScriptAsync("pause()");
        }

        private void PlayVideo(string id)
        {
            webView.ExecuteScriptAsync($"loadVideoById(\"{id}\",0)");
        }


    }
}
