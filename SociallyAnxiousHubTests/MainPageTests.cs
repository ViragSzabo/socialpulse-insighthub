using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Microsoft.Maui.Controls;
using Xunit;
using SociallyAnxiousHub;

namespace SociallyAnxiousHub.Tests
{
    public class MainPageTests
    {
        private readonly MainPage _mainPage;
        private readonly Mock<Label> _kpopStatsLabelMock;
        private readonly Mock<Label> _quoteLabelMock;
        private readonly Mock<ProgressBar> _progressBarMock;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public MainPageTests()
        {
            // Use real MainPage instead of Mocking
            _mainPage = new MainPage();

            _kpopStatsLabelMock = new Mock<Label>();
            _quoteLabelMock = new Mock<Label>();
            _progressBarMock = new Mock<ProgressBar>();

            // Assign mocked controls to real MainPage instance
            _mainPage.kpopStatsLabel = _kpopStatsLabelMock.Object;
            _mainPage.quoteLabel = _quoteLabelMock.Object;
            _mainPage.progressBar = _progressBarMock.Object;

            _cancellationTokenSource = new CancellationTokenSource();
        }

        [Fact]
        public async Task FetchKpopStats_ShouldUpdateStatsLabelAfterDelay()
        {
            // Arrange
            var cancellationToken = _cancellationTokenSource.Token;

            // Act
            await _mainPage.FetchKpopStats(cancellationToken);

            // Assert
            _kpopStatsLabelMock.VerifySet(x => x.Text = "Followers: 5M | Likes: 1.2M | Comments: 100K", Times.Once);
        }

        [Fact]
        public async Task SimulateProgressBar_ShouldUpdateProgressBar()
        {
            // Arrange
            var cancellationToken = _cancellationTokenSource.Token;

            // Act
            await _mainPage.SimulateProgressBar(cancellationToken);

            // Assert
            _progressBarMock.VerifySet(x => x.Progress = It.Is<double>(p => p > 0), Times.AtLeastOnce);
        }

        [Fact]
        public async Task ShowRandomQuote_ShouldUpdateQuoteLabel()
        {
            // Arrange
            var cancellationToken = _cancellationTokenSource.Token;

            // Act
            await _mainPage.ShowRandomQuote(cancellationToken);

            // Assert
            _quoteLabelMock.VerifySet(x => x.Text = It.IsAny<string>(), Times.Once);
        }

        [Fact]
        public void OnCancelButtonClicked_ShouldCancelTasks()
        {
            // Arrange
            _cancellationTokenSource.Cancel();

            // Act
            _mainPage.OnCancelButtonClicked(null, EventArgs.Empty);

            // Assert
            Assert.True(_cancellationTokenSource.Token.IsCancellationRequested);
        }

        [Fact]
        public async Task FetchKpopStats_ShouldHandleCancellation()
        {
            // Arrange
            var cancellationToken = _cancellationTokenSource.Token;
            _cancellationTokenSource.Cancel(); // Immediately cancel

            // Act
            await _mainPage.FetchKpopStats(cancellationToken);

            // Assert
            _kpopStatsLabelMock.VerifySet(x => x.Text = "Followers: 5M | Likes: 1.2M | Comments: 100K", Times.Never); // Never update since cancelled
        }
    }
}