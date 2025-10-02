using SociallyAnxiousHub.Features;
using SociallyAnxiousHub.Services;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SociallyAnxiousHub.Pages.MemoryBoardPage
{
    public partial class MemoryBoardViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        private string _boardName;

        [ObservableProperty]
        private string _boardDescription;

        [ObservableProperty]
        private ObservableCollection<MemoryItem> _memoryItems;

        public MemoryBoardViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            BoardName = "My Memory Board";
            BoardDescription = "A collection of my cherished memories.";
            MemoryItems = new ObservableCollection<MemoryItem>();
            LoadMemoriesFromDatabase();
        }

        // Command to add a new memory
        [RelayCommand]
        private async Task AddMemoryAsync()
        {
            var newMemory = new MemoryItem { Title = $"New Memory {MemoryItems.Count + 1}" };
            MemoryItems.Add(newMemory);
            await _databaseService.SaveMemoryAsync(newMemory);
        }

        private async void LoadMemoriesFromDatabase()
        {
            var memories = await _databaseService.GetMemoriesAsync();
            foreach (var memory in memories)
            {
                MemoryItems.Add(memory);
            }
        }
    }
}