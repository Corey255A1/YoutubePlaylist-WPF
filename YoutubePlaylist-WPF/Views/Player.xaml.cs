using Microsoft.Web.WebView2.Core;
using System;
using System.Windows;
using System.Windows.Controls;

namespace YoutubePlaylistWPF.Views
{
    public partial class Player : UserControl
    {

        public string YoutubeTitle
        {
            get { return (string)GetValue(YoutubeTitleProperty); }
            set { SetValue(YoutubeTitleProperty, value); }
        }

        public static readonly DependencyProperty YoutubeTitleProperty =
            DependencyProperty.Register("YoutubeTitle", typeof(string), typeof(Player), new PropertyMetadata(""));


        public string WebViewHTMLPath
        {
            get { return (string)GetValue(WebViewHTMLPathProperty); }
            set { SetValue(WebViewHTMLPathProperty, value); }
        }

        public static readonly DependencyProperty WebViewHTMLPathProperty =
            DependencyProperty.Register("WebViewHTMLPath", typeof(string), typeof(Player), new PropertyMetadata(""));


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
            //string[] response = e.WebMessageAsJson.Trim('"').Split(':', 2);
            Data.YoutubeAPIStatus? status = System.Text.Json.JsonSerializer.Deserialize<Data.YoutubeAPIStatus>(e.WebMessageAsJson);
            YoutubeTitle = status?.Title ?? "ERROR";
        }

        private void PlayClick(object sender, System.Windows.RoutedEventArgs e)
        {
            webView.ExecuteScriptAsync("play()");
        }

        private void PauseClick(object sender, System.Windows.RoutedEventArgs e)
        {
            webView.ExecuteScriptAsync("pause()");
        }
    }
}
