using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace SociallyAnxiousHub.Services
{
    using System.Net.Http.Json;

    public class InstagramRapidApiService : IInstagramService
    {
        private readonly HttpClient _http;

        public InstagramRapidApiService(HttpClient http)
        {
            _http = http; // Assign the injected HttpClient instance to the _http field.
            _http.BaseAddress = new Uri("https://instagram-premium-api-2023.p.rapidapi.com/v2/story/by/url");
            _http.DefaultRequestHeaders.Add("X-RapidAPI-Key", "12e674a2bamsh0b07da9e9e77690p135453jsne393fea6ef1a");
            _http.DefaultRequestHeaders.Add("X-RapidAPI-Host", "instagram-premium-api-2023.p.rapidapi.com");
        }

        public async Task<InstagramProfile> GetProfileAsync(string username, CancellationToken ct = default)
        {
            var url = $"https://{_http.BaseAddress.Host}/profile?username={Uri.EscapeDataString(username)}";
            var response = await _http.GetFromJsonAsync<InstagramProfile>(url, ct);
            return response!;
        }
    }
}