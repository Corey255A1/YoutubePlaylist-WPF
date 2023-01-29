using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using YoutubePlaylistWPF.Data;

namespace YoutubePlaylistWPF.Controllers
{
    public class PlaylistController : INotifyPropertyChanged
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
            set
            {
                _currentPlaylistItem = value;
                Notify();
            }
        }


        public PlaylistCommand PlayItemCommand { get; set; }
        public PlaylistCommand RemoveItemCommand { get; set; }
        public PlaylistCommand MoveItemUpCommand { get; set; }
        public PlaylistCommand MoveItemDownCommand { get; set; }

        public PlaylistController()
        {
            PlayItemCommand = new PlaylistCommand(PlayItem);
            RemoveItemCommand = new PlaylistCommand(RemoveItem);
            MoveItemUpCommand = new PlaylistCommand(MoveItemUp);
            MoveItemDownCommand = new PlaylistCommand(MoveItemDown);

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

        public void PlayItem(PlaylistItem item)
        {
            CurrentPlaylistItem = item;
        }
        public void RemoveItem(PlaylistItem item)
        {
            Playlist.Remove(item);
        }
        public void MoveItemUp(PlaylistItem item)
        {
            int itemIndex = Playlist.IndexOf(item);
            if ((itemIndex - 1) < 0) { return; }

            Playlist.Move(itemIndex, itemIndex - 1);
        }
        public void MoveItemDown(PlaylistItem item)
        {
            int itemIndex = Playlist.IndexOf(item);
            if ((itemIndex + 1) >= Playlist.Count) { return; }

            Playlist.Move(itemIndex, itemIndex + 1);
        }
    }
}
