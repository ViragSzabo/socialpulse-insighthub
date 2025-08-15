using SociallyAnxiousHub.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SociallyAnxiousHub.ViewModels
{
    public class MainViewModel1 : INotifyPropertyChanged
    {
        CancellationTokenSource _cts;

        public ICommand FetchCommand { get; }
        public ICommand CancelCommand { get; }
        public IInstagramService _instagram;

        bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set { SetField(ref _isBusy, value); ((Command)FetchCommand).ChangeCanExecute(); }
        }

        public MainViewModel1(IInstagramService instagram)
        {
            _instagram = instagram ?? throw new ArgumentNullException(nameof(instagram));
            FetchCommand = new Command(async () => await FetchAll());
            CancelCommand = new Command(() => _cts?.Cancel());
        }

        string _statsText = "Fetching K-pop stats...";
        public string StatsText
        {
            get => _statsText;
            set => SetField(ref _statsText, value);
        }

        string _quoteText = "Random quote here…";
        public string QuoteText
        {
            get => _quoteText;
            set => SetField(ref _quoteText, value);
        }

        double _progress;

        public double Progress
        {
            get => _progress;
            set => SetField(ref _progress, value);
        }

        public async Task FetchAll()
        {
            _isBusy = true;
            ((ICommand)FetchCommand).ChangeCanExecute();
            StatsText = "Loading Instagram…";
            QuoteText = "Fetching quote…";
            _cts = new CancellationTokenSource();

            try
            {
                var profile = await _instagram.GetProfileAsync("lisa_official", _cts.Token);
                StatsText = $"👥 {profile.FollowersCount:N0}   📝 {profile.PostsCount}";
            }
            catch (OperationCanceledException)
            {
                StatsText = "Canceled";
            }
            catch (Exception ex)
            {
                StatsText = $"Error: {ex.Message}";
            }
            finally
            {
                _isBusy = false;
                ((ICommand)FetchCommand).ChangeCanExecute();
            }
        }

        public async Task FetchStats(CancellationToken token)
        {
            await Task.Delay(3000, token);
            if (token.IsCancellationRequested) return;
            StatsText = "Followers: 5M | Likes: 1.2M | Comments: 100K";
        }

        public async Task FetchAllInternal()
        {
            _cts = new CancellationTokenSource();
            var token = _cts.Token;
            // reset…
            StatsText = "Fetching K-pop stats…";
            QuoteText = "Random quote here…";
            try
            {
                await Task.WhenAll(
                  FetchStats(token),
                  SimulateProgress(token),
                  ShowQuote(token)
                );
            }
            catch (OperationCanceledException)
            {
                StatsText = "Operation was canceled.";
            }
        }

        public async Task SimulateProgress(CancellationToken token)
        {
            Progress = 0;
            while (Progress < 1.0)
            {
                if (token.IsCancellationRequested) return;
                Progress += 0.05;
                await Task.Delay(100, token);
            }
        }

        public async Task ShowQuote(CancellationToken token)
        {
            var quotes = new List<string>
            {
                "Grandpa's wisdom: 'Don't sweat the small stuff.'",
                "'The Pitt' says: 'Keep going even when it's hard.'"
            };
            await Task.Delay(1000, token);
            if (token.IsCancellationRequested) return;
            var rnd = new Random();
            QuoteText = quotes[rnd.Next(quotes.Count)];
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string name = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            return true;
        }
        #endregion
    }
}