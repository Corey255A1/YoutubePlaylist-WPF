using System;
using System.Windows.Input;
using YoutubePlaylistWPF.Data;

namespace YoutubePlaylistWPF.Controllers
{
    public delegate void PlaylistActionCallback(PlaylistItem item);
    public class PlaylistItemCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private PlaylistActionCallback _callback;

        public PlaylistItemCommand(PlaylistActionCallback callback)
        {
            _callback = callback;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter == null) { return; }
            _callback.Invoke((PlaylistItem)parameter);
        }
    }
    public class PlaylistControlCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private Action _callback;

        public PlaylistControlCommand(Action callback)
        {
            _callback = callback;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _callback.Invoke();
        }
    }
}
