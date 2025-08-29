using SociallyAnxiousHub.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Authentication; // MAUI Essentials WebAuthenticator

namespace SociallyAnxiousHub.Authentication
{
    class SpotifyService
    {
        private string _accessToken;
        private string _refreshToken;
        private string _codeVerifier; // PKCE code verifier
        private string _clientId;
        private readonly HttpClient _httpClient = new HttpClient();
        private const string TOKEN_URL = "https://accounts.spotify.com/api/token";
        private const string API_BASE_URL = "https://api.spotify.com/v1/";
        private const string REDIRECT_URI = "sociallyanxioushub://callback"; // Must be registered in Spotify Dev Dashboard

        public SpotifyService()
        {
            // Initialize HttpClient and load client ID
            _httpClient.BaseAddress = new Uri(API_BASE_URL);
        }

        public async Task InitializeAsync()
        {
            // Load client ID from secure storage
            _clientId = await SecureStorage.GetAsync("spotify_client_id");
            _refreshToken = await SecureStorage.GetAsync("spotify_refresh_token");
        }

        public async Task AuthenticateAsync()
        {
            try
            {
                // 1. Generate the PKCE keys
                _codeVerifier = GenerateCodeVerifier();
                var codeChallenge = GenerateCodeChallenge(codeVerifier); 
                var scopes = "user-read-private playlist-read-private";

                // response_type=code for Authorization Code Flow
                var authUrl = $"https://accounts.spotify.com/authorize?client_id={_clientId}&response_type=code&redirect_uri={Uri.EscapeDataString(REDIRECT_URI)}&scope={Uri.EscapeDataString(scopes)}&code_challenge={codeChallenge}&code_challenge_method=S256";
                var authResult = await WebAuthenticator.AuthenticateAsync(new Uri(authUrl), new Uri(REDIRECT_URI));

                var code = authResult.Properties["code"];

                // 2. Pass the code verifier to the token exchange method
                await ExchangeCodeForTokenAsync(code, codeVerifier);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Authentication failed: {ex.Message}");
            }
        }

        private async Task ExchangeCodeForTokenAsync(string code, string codeVerifier)
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, TOKEN_URL);
            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "authorization_code" },
                { "code", code },
                { "redirect_uri", REDIRECT_URI },
                { "client_id", clientId },

                // 3. Instead of client_secret, send the code_verifier
                { "code_verifier", codeVerifier }
            });

            // 4. Send the request and handle the response
            using var response = await _httpClient.SendAsync(request); // Using the same HttpClient instance avoiding unnecessary instantiation
            response.EnsureSuccessStatusCode(); // Throw if not a success code.

            var content = await response.Content.ReadAsStringAsync();
            var tokenResponse = System.Text.Json.JsonSerializer.Deserialize<TokenResponse>(content);

            _accessToken = tokenResponse.access_token;
            _refreshToken = tokenResponse.refresh_token;

            // Store tokens securely
            if (!string.IsNullOrEmpty(_refreshToken))
                await SecureStorage.SetAsync("spotify_refresh_token", _refreshToken);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
        }

        // Generates a code challenge from the code verifier.
        private string GenerateCodeVerifier()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-._~";
            var bytes = new byte[128];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);

            var result = new char[128];
            for (int i = 0; i < 128; i++)
            {
                result[i] = chars[bytes[i] % chars.Length];
            }

            return new string(result);
        }

        // Generates a code challenge from the code verifier using SHA-256 and Base64 URL encoding.
        private string GenerateCodeChallenge(string codeVerifier)
        {
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(codeVerifier));
            var base64 = Convert.ToBase64String(hash);
            return base64.Replace('+', '-').Replace('/', '_').Replace("=", "");
        }

        private class TokenResponse
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public int expires_in { get; set; }
            public string refresh_token { get; set; }
        }

        public async Task<List<Song>> SearchSongsAsync(string query)
        {
            try
            {
                var response = await _httpClient.GetAsync($"search?q={Uri.EscapeDataString(query)}&type=track");
                return await ParseSpotifyResponse(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching for songs: {ex.Message}");
                return new List<Song>();
            }
        }

        private async Task<List<Song>> ParseSpotifyResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error parsing Spotify response: {response.StatusCode}");
                return new List<Song>();
            }

            var content = await response.Content.ReadAsStringAsync();
            var json = System.Text.Json.JsonDocument.Parse(content);
            var songs = new List<Song>();

            foreach (var item in json.RootElement.GetProperty("tracks").GetProperty("items").EnumerateArray())
            {
                var title = item.GetProperty("name").GetString();
                var artist = item.GetProperty("artists").EnumerateArray().FirstOrDefault().GetProperty("name").GetString();
                var album = item.GetProperty("album").GetProperty("name").GetString();
                var duration = TimeSpan.FromMilliseconds(item.GetProperty("duration_ms").GetInt32());
                var coverImageUrl = item.GetProperty("album").GetProperty("images").EnumerateArray().FirstOrDefault().GetProperty("url").GetString();
                var spotifyUrl = item.GetProperty("external_urls").GetProperty("spotify").GetString();

                songs.Add(new Song(title, artist, album, duration, "Unknown", coverImageUrl, spotifyUrl));
            }

            return songs;
        }

        // Refresh the access token using the refresh token
        private async Task RefreshAccessTokenAsync()
        {
            var request = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "refresh_token" },
                { "refresh_token", _refreshToken },
                { "client_id", clientId }
            });

            // Reuse the same HttpClient instance
            var response = await _httpClient.PostAsync(TOKEN_URL, request);
            response.EnsureSuccessStatusCode();

            // Update the access token
            var content = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(content);
            _accessToken = tokenResponse.access_token;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
        }

        // Ensure we have a valid access token before making API calls
        private async Task EnsureAccessTokenAsync()
        {
            if (string.IsNullOrEmpty(_accessToken))
            {
                if (!string.IsNullOrEmpty(_refreshToken))
                    await RefreshAccessTokenAsync();
                else
                    throw new InvalidOperationException("User not authenticated.");
            }
        }
    }
}