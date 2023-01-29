using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
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


        public ICommand PlayItemCommand { get; set; }
        public ICommand PlayNextItemCommand { get; set; }
        public ICommand PlayPreviousItemCommand { get; set; }
        public ICommand RemoveItemCommand { get; set; }
        public ICommand MoveItemUpCommand { get; set; }
        public ICommand MoveItemDownCommand { get; set; }
        public ICommand AddItemToTopCommand { get; set; }
        public ICommand AddItemToBottomCommand { get; set; }

        public PlaylistController()
        {
            PlayItemCommand = new PlaylistItemCommand(PlayItem);
            
            RemoveItemCommand = new PlaylistItemCommand(RemoveItem);
            MoveItemUpCommand = new PlaylistItemCommand(MoveItemUp);
            MoveItemDownCommand = new PlaylistItemCommand(MoveItemDown);

            AddItemToTopCommand = new PlaylistControlCommand(AddItemToTop);
            AddItemToBottomCommand = new PlaylistControlCommand(AddItemToBottom);

            PlayNextItemCommand = new PlaylistControlCommand(PlayNextItem);
            PlayPreviousItemCommand = new PlaylistControlCommand(PlayPreviousItem);

            Playlist = new ObservableCollection<PlaylistItem>()
            {
                new PlaylistItem()
                {
                    URL="KFBgEsjaJoc",
                    ID="1"
                },
                new PlaylistItem()
                {
                    URL="jX21YCADuTA",
                    ID="2"
                },
                new PlaylistItem()
                {
                    URL="oC3DuSahiZc",
                    ID="3"
                }
            };

            CurrentPlaylistItem = Playlist[1];
        }

        public void AddItemToTop()
        {
            var item = new PlaylistItem() { ID = Guid.NewGuid().ToString() };
            Playlist.Insert(0, item);
        }

        public void AddItemToBottom()
        {
            var item = new PlaylistItem() { ID = Guid.NewGuid().ToString() };
            Playlist.Add(item);
        }

        public void PlayItem(PlaylistItem item)
        {
            CurrentPlaylistItem = item;
        }

        public void PlayNextItem()
        {
            int itemIndex = -1;
            if (CurrentPlaylistItem != null)
            {
                itemIndex = Playlist.IndexOf(CurrentPlaylistItem);
            }
            
            if ((itemIndex + 1) >= Playlist.Count) { return; }

            PlayItem(Playlist[itemIndex + 1]);
        }

        public void PlayPreviousItem()
        {
            int itemIndex = Playlist.Count;
            if (CurrentPlaylistItem != null)
            {
                itemIndex = Playlist.IndexOf(CurrentPlaylistItem);
            }

            if ((itemIndex - 1) < 0) { return; }

            PlayItem(Playlist[itemIndex - 1]);
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
