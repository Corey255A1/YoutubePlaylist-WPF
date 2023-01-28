using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using YoutubePlaylistWPF.Data;

namespace YoutubePlaylistWPF.Controllers
{
    public class PlaylistController: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void Notify([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ObservableCollection<PlaylistItem> Playlist { get; set; }

        private PlaylistItem? _currentPlaylistItem;
        public PlaylistItem? CurrentPlaylistItem
        {
            get { return _currentPlaylistItem; }
            set {
                _currentPlaylistItem = value;
                Notify();
            }
        }


        public PlaylistCommand PlayItemCommand { get; set; }

        public PlaylistController() {
            PlayItemCommand = new PlaylistCommand(PlayItem);
            Playlist = new ObservableCollection<PlaylistItem>()
            {
                new PlaylistItem()
                {
                    URL="KFBgEsjaJoc",
                    ID=1
                },
                new PlaylistItem()
                {
                    URL="jX21YCADuTA",
                    ID=2
                },
                new PlaylistItem()
                {
                    URL="oC3DuSahiZc",
                    ID=3
                }
            };

            CurrentPlaylistItem = Playlist[1];
        }

        private void PlayItem(PlaylistItem item)
        {
            CurrentPlaylistItem = item;
        }
    }
}
