using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.ApplicationModel;

namespace SociallyAnxiousHub.Authentication
{
    public class LyricsService
    {
        private readonly HttpClient _httpClient = new HttpClient(); // Reuse HttpClient instance
        private const string API_BASE_URL = "https://api.musixmatch.com/ws/1.1/"; // Musixmatch API base URL
        private readonly string _apiKey; // Musixmatch API key

        // Constructor to initialize HttpClient and API key
        public LyricsService(string apikey)
        {
            _httpClient.BaseAddress = new Uri(API_BASE_URL); // Set base address
            _apiKey = apikey; // Initialize API key
        }

        // Method to get lyrics by artist and title
        public async Task<string> GetLyricsAsync(string artist, string title)
        {
            // Prepare request URL
            var url = $"{API_BASE_URL}matcher.lyrics.get?q_track={Uri.EscapeDataString(title)}&q_artist={Uri.EscapeDataString(artist)}&apikey={_apiKey}";
            var response = await _httpClient.GetAsync(url); // Make GET request

            if (response.IsSuccessStatusCode) // Check if response is successful
            {
                var content = await response.Content.ReadAsStringAsync();
                using var json = JsonDocument.Parse(content); // Parse JSON response
                var lyrics = json.RootElement // Navigate to lyrics in JSON response
                    .GetProperty("message")
                    .GetProperty("body")
                    .GetProperty("lyrics")
                    .GetProperty("lyrics_body")
                    .GetString();

                return lyrics ?? "Lyrics not found."; // Return lyrics or not found message
            }
            MathThread.BeginInvokeOnMainThread(() => { $"Failed Fetching lyrics: " });
            return "Error fetching lyrics."; // Return error message
        }

        // Class to parse lyrics response
        private class LyricsResponse
        {
            public string Lyrics { get; set; }
        }
    }
}