using SociallyAnxiousHub.Authentication;
using SociallyAnxiousHub.Features;
using SociallyAnxiousHub.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SociallyAnxious.Pages.PlaylistPage
{
    public partial class PlayListViewModel : ObservableObject
    {
        private readonly SpotifyService _spotifyService;
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        private string _playlistName;

        [ObservableProperty]
        private string _playlistDescription;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SearchCommand))]
        private string _searchQuery;

        [ObservableProperty]
        private ObservableCollection<Song> _searchResults;

        [ObservableProperty]
        private ObservableCollection<Song> _currentPlaylist;

        public PlayListViewModel(SpotifyService spotifyService, DatabaseService databaseService)
        {
            _spotifyService = spotifyService;
            _databaseService = databaseService;
            PlaylistName = "My Playlist";
            PlaylistDescription = "A collection of my favorite tracks.";
            SearchResults = new ObservableCollection<Song>();
            CurrentPlaylist = new ObservableCollection<Song>();
            LoadPlaylistFromDatabase();
        }

        // This is the new search command
        [RelayCommand]
        private async Task SearchAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery)) return;
            var songs = await _spotifyService.SearchSongsAsync(SearchQuery);
            SearchResults.Clear();
            foreach (var song in songs)
            {
                SearchResults.Add(song);
            }
        }

        // This command adds a song to the current playlist
        [RelayCommand]
        private async Task AddSongToPlaylistAsync(Song song)
        {
            if (song != null)
            {
                CurrentPlaylist.Add(song);
                await _databaseService.SaveSongsAsync(CurrentPlaylist.ToList());
            }
        }

        // This command removes a song from the current playlist
        [RelayCommand]
        private async Task RemoveSongFromPlaylistAsync(Song song)
        {
            if (song != null)
            {
                CurrentPlaylist.Remove(song);
                await _databaseService.SaveSongsAsync(CurrentPlaylist.ToList());
            }
        }

        private async void LoadPlaylistFromDatabase()
        {
            var songs = await _databaseService.GetSongsAsync();
            foreach (var song in songs)
            {
                CurrentPlaylist.Add(song);
            }
        }
    }
}