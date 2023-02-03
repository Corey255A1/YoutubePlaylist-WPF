using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace YoutubePlaylistWPF.Data
{
    public enum YoutubePlayerState
    {
        Unstarted = -1,
        Ended = 0,
        Playing = 1,
        Buffering = 3,
        Paused = 2,
        Queued = 5
    }
    public class YoutubeAPIStatus
    {
        [JsonPropertyName("title")]
        public string Title { get; set; } = "";
        [JsonPropertyName("state")]
        public YoutubePlayerState State { get; set; }
    }
}
