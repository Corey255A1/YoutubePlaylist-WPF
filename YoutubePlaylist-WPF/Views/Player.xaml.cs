using Microsoft.Web.WebView2.Core;
using System;
using System.Windows;
using System.Windows.Controls;

namespace YoutubePlaylistWPF.Views
{
    /// <summary>
    /// Interaction logic for Player.xaml
    /// </summary>
    public partial class Player : UserControl
    {


        public string YoutubeTitle
        {
            get { return (string)GetValue(YoutubeTitleProperty); }
            set { SetValue(YoutubeTitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for YoutubeTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty YoutubeTitleProperty =
            DependencyProperty.Register("YoutubeTitle", typeof(string), typeof(Player), new PropertyMetadata(""));



        private static readonly string TESTIFRAME =
@"
<html>

<head>
    <style>
        html,
        body {
            background: lightgray;
            overflow: hidden;
            padding: 0;
            margin: 0;
        }

        iframe {
            border: none;
            margin: 0;
            padding: 0;
        }
    </style>
</head>

<body>
    <iframe width="" 300"" height="" 300"" src=""https://www.youtube.com/embed/KFBgEsjaJoc?enablejsapi=1&mute=1""
        allow="" accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share""
        allowFullScreen id=""wpf-player""></iframe>
    <script>
        let youtubePlayer = null;
        function playerStateChangeHandler(e) {
            chrome.webview.postMessage(`${e.data}:${e.target.videoTitle}`);

        }
        function play() {
            if (youtubePlayer != null) { youtubePlayer.playVideo(); }
        }
        function pause() {
            if (youtubePlayer != null) { youtubePlayer.pauseVideo(); }
        }

        function loadVideoById(videoId, startSeconds) {
            if (youtubePlayer != null) { youtubePlayer.loadVideoById(videoId, startSeconds); }
        }

        const tag = document.createElement('script');
        tag.src = 'https://www.youtube.com/iframe_api';

        // onYouTubeIframeAPIReady will load the video after the script is loaded
        window.onYouTubeIframeAPIReady = () => {
            const playerFrame = new window.YT.Player('wpf-player',
                {
                    events: {
                        'onReady': (event) => { youtubePlayer = event.target },
                        'onStateChange': playerStateChangeHandler
                    }
                });

        }

        const firstScriptTag = document.getElementsByTagName('script')[0];
        firstScriptTag.parentNode?.insertBefore(tag, firstScriptTag);
    </script>
</body>

</html>
";
        public Player()
        {
            InitializeComponent();
            Loaded += PlayerLoaded;
        }

        private async void PlayerLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await webView.EnsureCoreWebView2Async();
            webView.NavigateToString(TESTIFRAME);
            webView.CoreWebView2.WebMessageReceived += YoutubeWebviewWebMessageReceived;
        }

        private void YoutubeWebviewWebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            string[] response = e.WebMessageAsJson.Trim('"').Split(':', 2);
            YoutubeTitle = response[1];
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
