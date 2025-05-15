using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace SociallyAnxiousHub
{
    public partial class MainPage : ContentPage
    {
        private CancellationTokenSource _cancellationTokenSource;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnFetchKpopStatsClicked(object sender, EventArgs e)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = _cancellationTokenSource.Token;

            try
            {
                // Reset UI before fetching new data
                kpopStatsLabel.Text = "Fetching K-pop stats...";
                quoteLabel.Text = "Random quote here...";

                await Task.WhenAll(
                    FetchKpopStats(cancellationToken),
                    SimulateProgressBar(cancellationToken),
                    ShowRandomQuote(cancellationToken)
                );
            }
            catch (OperationCanceledException)
            {
                kpopStatsLabel.Text = "Operation was canceled.";
            }
        }

        private async Task FetchKpopStats(CancellationToken cancellationToken)
        {
            await Task.Delay(3000, cancellationToken); // Simulate a delay
            if (cancellationToken.IsCancellationRequested) return;

            MainThread.BeginInvokeOnMainThread(() =>
            {
                kpopStatsLabel.Text = "Followers: 5M | Likes: 1.2M | Comments: 100K";
            });
        }

        private async Task SimulateProgressBar(CancellationToken cancellationToken)
        {
            int progress = 0;
            while (progress < 100)
            {
                if (cancellationToken.IsCancellationRequested) return;

                progress += 5;
                progressBar.Progress = progress / 100.0;
                await Task.Delay(100, cancellationToken); // Asynchronous progress update
            }
        }

        private async Task ShowRandomQuote(CancellationToken cancellationToken)
        {
            var quotes = new List<string>
            {
                "Grandpa's wisdom: 'Don't sweat the small stuff.'",
                "'The Pitt' says: 'Keep going even when it's hard.'"
            };

            await Task.Delay(1000, cancellationToken); // Simulate delay for fetching a quote
            if (cancellationToken.IsCancellationRequested) return;

            var random = new Random();
            var quote = quotes[random.Next(quotes.Count)];

            MainThread.BeginInvokeOnMainThread(() =>
            {
                quoteLabel.Text = quote;
            });
        }

        // In case the user cancels the ongoing operation
        private void OnCancelButtonClicked(object sender, EventArgs e)
        {
            _cancellationTokenSource?.Cancel();
        }
    }
}