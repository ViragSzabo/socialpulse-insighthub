namespace SociallyAnxiousHub.Services
{
    public interface IInstagramService
    {
        Task<InstagramProfile> GetProfileAsync(string username, CancellationToken ct = default);
    }

    public class InstagramProfile
    {
        public string? Username { get; set; }
        public string? FullName { get; set; }
        public string? Bio { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }
        public int PostsCount { get; set; }
        public string? ProfilePictureUrl { get; set; }
    }
}