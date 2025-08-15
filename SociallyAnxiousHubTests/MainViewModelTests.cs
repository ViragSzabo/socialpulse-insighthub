using System.Threading.Tasks;
using Xunit;
using SociallyAnxiousHub.ViewModels;

namespace SociallyAnxiousHub.Tests
{
    public class MainViewModelTests
    {
        [Fact]
        public async Task FetchStats_UpdatesStatsText()
        {
            var vm = new MainViewModel();
            await vm.FetchAll();

            Assert.AreEqual(
                "Followers: 5M | Likes: 1.2M | Comments: 100K",
                vm.StatsText
            );
        }

        [Fact]
        public async Task SimulateProgress_UpdatesProgress()
        {
            var vm = new MainViewModel();
            await vm.FetchAll();

            Assert.IsTrue(vm.Progress >= 1.0);
        }

        [Fact]
        public async Task ShowQuote_UpdatesQuoteText()
        {
            var vm = new MainViewModel();
            await vm.FetchAll();

            Assert.AreNotEqual("Random quote here…", vm.QuoteText);
        }

        [Fact]
        public async Task CancelCommand_StopsOperation()
        {
            var vm = new MainViewModel();
            // start fetch but cancel immediately
            var fetchTask = vm.FetchAll();
            vm.CancelCommand.Execute(null);
            await fetchTask;

            // after cancellation the StatsText should NOT be the final value
            Assert.AreNotEqual(
                "Followers: 5M | Likes: 1.2M | Comments: 100K",
                vm.StatsText
            );
        }
    }
}