using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SociallyAnxiousHub.Features;

namespace SociallyAnxiousHub.Database
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            
            var dbService = new DatabaseService(dbPath);
            await dbService.InitializeAsync();
        }

        // Initialize tables
        public async Task InitializeAsync()
        {
            await _database.CreateTableAsync<UserProfileDb>();
            await _database.CreateTableAsync<MemoryDb>();
            await _database.CreateTableAsync<MoodEntryDb>();
            await _database.CreateTableAsync<HabitDb>();
            await _database.CreateTableAsync<PlaylistDb>();
            await _database.CreateTableAsync<ChallengeDb>();
            await _database.CreateTableAsync<BadgeDb>();
        }

        // User profile methods
        public Task<int> SaveUserProfileAsync(UserProfileDb profile) =>
            _database.InsertOrReplaceAsync(profile);

        public Task<List<UserProfileDb>> GetUserProfilesAsync() =>
            _database.Table<UserProfileDb>().ToListAsync();

        // Memory methods
        public Task<int> SaveMemoryAsync(MemoryDb memory) =>
            _database.InsertOrReplaceAsync(memory);

        public Task<List<MemoryDb>> GetMemoriesAsync() =>
            _database.Table<MemoryDb>().ToListAsync();

        public Task<int> DeleteMemoryAsync(MemoryDb memory) =>
            _database.DeleteAsync(memory);

        // Mood methods
        public Task<int> SaveMoodAsync(MoodEntryDb mood) =>
            _database.InsertOrReplaceAsync(mood);

        public Task<List<MoodEntryDb>> GetMoodsAsync() =>
            _database.Table<MoodEntryDb>().ToListAsync();

        // Habit methods
        public Task<int> SaveHabitAsync(HabitDb habit) =>
            _database.InsertOrReplaceAsync(habit);

        public Task<List<HabitDb>> GetHabitsAsync() =>
            _database.Table<HabitDb>().ToListAsync();

        // Playlist methods
        public Task<int> SavePlaylistAsync(PlaylistDb playlist) =>
            _database.InsertOrReplaceAsync(playlist);

        public Task<List<PlaylistDb>> GetPlaylistsAsync() =>
            _database.Table<PlaylistDb>().ToListAsync();

        public static PlaylistDb ToDbModel(Playlist p) => new PlaylistDb
        {
            Name = p.Name,
            Description = p.Description,
            SpotifyId = p.SpotifyUrl
        };

        public static Playlist ToFeatureModel(PlaylistDb db) => new Playlist
        {
            Name = db.Name,
            Description = db.Description,
            SpotifyUrl = db.SpotifyId
        };

        // Challenge methods
        public Task<int> SaveChallengeAsync(ChallengeDb challenge) =>
            _database.InsertOrReplaceAsync(challenge);

        public Task<List<ChallengeDb>> GetChallengesAsync() =>
            _database.Table<ChallengeDb>().ToListAsync();

        public static ChallengeDb ToDbModel(Challenge c) => new ChallengeDb
        {
            Title = c.Title,
            Description = c.Description,
            IsCompleted = c.isCompleted
        };

        public static Challenge ToFeatureModel(ChallengeDb db) => new Challenge
        {
            Title = db.Title,
            Description = db.Description,
            IsCompleted = db.IsCompleted
        };

        // Badge methods
        public Task<int> SaveBadgeAsync(BadgeDb badge) =>
            _database.InsertOrReplaceAsync(badge);

        public Task<List<BadgeDb>> GetBadgesAsync() =>
            _database.Table<BadgeDb>().ToListAsync();

        public static BadgeDb ToDbModel(Badge b) => new Badge
        {
            Title = b.Title,
            Description = b.Description,
            Unlocked = b.Unlocked
        };

        public static Challenge ToFeatureModel(BadgeDb db) => new Badge
        {
            Title = db.Title,
            Description = db.Description,
            Unlocked = db.Unlocked
        };
    }
}

// Database Models
[Table("UserProfiles")]
public class UserProfileDb
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string AvatarPath { get; set; }
}

[Table("Memories")]
public class MemoryDb
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string ImagePath { get; set; }
}

[Table("MoodEntries")]
public class MoodEntryDb
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int MoodLevel { get; set; } // e.g., 1-10 scale
    public string Notes { get; set; }
}

[Table("Habits")]
public class HabitDb
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime Date { get; set; }
}

[Table("Playlists")]
public class PlaylistDb
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string SpotifyId { get; set; }
}

[Table("Challenges")]
public class ChallengeDb
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
}

[Table("Badges")]
public class BadgeDb
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Unlocked { get; set; }
}