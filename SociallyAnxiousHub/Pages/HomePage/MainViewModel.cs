using System;

namespace SociallyAnxiousHub.Pages.HomePage
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly SpotifyService _spotifyService;

        [ObservableProperty]
        private bool _isAuthenticated;

        public MainViewModel(SpotifyService spotifyService)
        {
            _spotifyService = spotifyService;
            CheckAuthenticationStatus();
        }

        private async void CheckAuthenticationStatus()
        {
            // Logic to check if the user is authenticated
            IsAuthenticated = _spotifyService.IsAuthenticated;
        }

        [RelayCommand]
        private async Task AuthenticateAsync()
        {
            try
            {
                await _spotifyService.AuthenticateAsync();
                IsAuthenticated = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Authentication failed: {ex.Message}");
                IsAuthenticated = false;
            }
        }

        [RelayCommand]
        private async Task GoToPlaylistAsync()
        {
            // Navigate to the Playlist page
            await Shell.Current.GoToAsync(nameof(PlayListPage));
        }

        [RelayCommand]
        private async Task GoToMemoryBoardAsync()
        {
            // Navigate to the Memory Board page
            await Shell.Current.GoToAsync(nameof(MemoryBoardPage));
        }
    }
}