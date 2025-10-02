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

            // Initialize tables
            _database.CreateTableAsync<UserProfileDb>().Wait();
            _database.CreateTableAsync<MemoryDb>().Wait();
            _database.CreateTableAsync<MoodEntryDb>().Wait();
            _database.CreateTableAsync<HabitDb>().Wait();
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
        public Task<int> SavePlaylistAsync(Playlist playlist) =>
            _database.InsertOrReplaceAsync(playlist);

        public Task<List<Playlist>> GetPlaylistsAsync() => 
            _database.Table<Playlist>().ToListAsync();

        // Challenge methods
        public Task<int> SaveChallengeAsync(Challenge challenge) =>
            _database.InsertOrReplaceAsync(challenge);

        public Task<List<Challenge>> GetChallengesAsync() => 
            _database.Table<Challenge>().ToListAsync();

        // Badge methods
        public Task<int> SaveBadgeAsync(Badge badge) =>
            _database.InsertOrReplaceAsync(badge);

        public Task<List<Badge>> GetBadgesAsync() =>
            _database.Table<Badge>().ToListAsync();
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
public class Playlist
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string SpotifyId { get; set; }
}

[Table("Challenges")]
public class Challenge
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
}

[Table("Badges")]
public class Badge
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
}