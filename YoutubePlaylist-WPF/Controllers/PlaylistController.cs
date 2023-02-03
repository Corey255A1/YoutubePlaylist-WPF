//Corey Wunderlich WunderVision 2023
//https://www.wundervisionenvisionthefuture.com/
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

        private string _title = "Nothing Playing";
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                Notify();
            }
        }

        private YoutubePlayerState _playerState;
        public YoutubePlayerState PlayerState
        {
            get { return _playerState; }
            set
            {
                _playerState = value;
                Notify();
            }
        }

        private bool _autoPlay;
        public bool AutoPlay
        {
            get { return _autoPlay; }
            set
            {
                _autoPlay = value;
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
        public ICommand PlayerStatusCommand { get; set; }

        public PlaylistController()
        {
            PlayItemCommand = new GenericCommand<PlaylistItem>(PlayItem);

            RemoveItemCommand = new GenericCommand<PlaylistItem>(RemoveItem);
            MoveItemUpCommand = new GenericCommand<PlaylistItem>(MoveItemUp);
            MoveItemDownCommand = new GenericCommand<PlaylistItem>(MoveItemDown);

            AddItemToTopCommand = new GenericCommand(AddItemToTop);
            AddItemToBottomCommand = new GenericCommand(AddItemToBottom);

            PlayNextItemCommand = new GenericCommand(PlayNextItem);
            PlayPreviousItemCommand = new GenericCommand(PlayPreviousItem);

            PlayerStatusCommand = new GenericCommand<YoutubeAPIStatus>(PlayerStatusHandler);

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

        public void PlayerStatusHandler(YoutubeAPIStatus status)
        {
            Title = status.Title;
            PlayerState = status.State;

            if(AutoPlay && PlayerState == YoutubePlayerState.Ended) {
                PlayNextItem();
            }
        }
    }
}
