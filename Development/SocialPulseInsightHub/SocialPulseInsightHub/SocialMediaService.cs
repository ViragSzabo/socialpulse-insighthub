public class SocialMediaService
{
    public async Task<string> GetSocialMediaDataAsync(string platform)
    {
        // Connect to API and fetch data (dummy data for now)
        await Task.Delay(1000); // Simulate async operation
        return $"Data for {platform}";
    }
}