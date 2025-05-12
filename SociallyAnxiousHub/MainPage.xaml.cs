using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace SociallyAnxiousHub
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnFetchKpopStatsClicked(object sender, EventArgs e)
        {
            await FetchKpopStats();
            await GenerateFakeStats();
            await SimulateProgressBar();
            await ShowRandomQuote();
        }

        private async Task FetchKpopStats()
        {
            await Task.Run(() => System.Threading.Thread.Sleep(3000));
            MainThread.BeginInvokeOnMainThread(() =>
            {
                kpopStatsLabel.Text = "Followers: 5M | Likes: 1.2M | Comments: 100K";
                // kpopStatsLabel.Text = "K-Pop Stats Fetched!";
            });
        }

        private async Task GenerateFakeStats()
        {
            Random random = new();
            await Task.Delay(2000);
            var followers = random.Next(1, 10) * 1000000;
            var likes = random.Next(100000, 1000000);
            var comments = random.Next(5000, 20000);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                kpopStatsLabel.Text = $"Followers: {followers} | Likes: {likes} | Comments: {comments}";
            });
        }

        private async Task SimulateProgressBar()
        {
            int progress = 0;
            while(progress < 100)
            {
                progress += 5;
                progressBar.Progress = progress / 100.0;
                await Task.Delay(100);
            }    
        }

        private async Task ShowRandomQuote()
        {
            var quotes = new List<string>
            {
                "Grandpa's wisdom: 'Don't sweat the small stuff.'",
                "'The Pitt' says: 'Keep going even when it's hard.'"
            };

            Random random = new();
            await Task.Delay(1000);

            var quote = quotes[random.Next(quotes.Count)];

            MainThread.BeginInvokeOnMainThread(() =>
            {
                quoteLabel.Text = quote;
            });
        }
    }
}