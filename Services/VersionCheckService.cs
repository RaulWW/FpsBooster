using System.Text.Json;
using System.Text.Json.Serialization;

namespace FpsBooster.Services;

public class VersionCheckService
{
    private readonly HttpClient _httpClient;
    private readonly string _repoOwner;
    private readonly string _repoName;
    private readonly string _currentVersion;

    public VersionCheckService(string repoOwner, string repoName, string currentVersion)
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "FpsBooster");
        _repoOwner = repoOwner;
        _repoName = repoName;
        _currentVersion = currentVersion.TrimStart('v'); // Remove 'v' prefix if present
    }

    public async Task<VersionCheckResult> CheckForUpdatesAsync()
    {
        try
        {
            string url = $"https://api.github.com/repos/{_repoOwner}/{_repoName}/releases/latest";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return new VersionCheckResult 
                { 
                    IsSuccess = false, 
                    ErrorMessage = $"Failed to check updates: {response.StatusCode}" 
                };
            }

            string json = await response.Content.ReadAsStringAsync();
            var release = JsonSerializer.Deserialize<GitHubRelease>(json);

            if (release == null || string.IsNullOrEmpty(release.TagName))
            {
                return new VersionCheckResult 
                { 
                    IsSuccess = false, 
                    ErrorMessage = "Failed to parse release information" 
                };
            }

            string latestVersion = release.TagName.TrimStart('v');
            bool isNewVersion = CompareVersions(latestVersion, _currentVersion) > 0;

            return new VersionCheckResult
            {
                IsSuccess = true,
                IsNewVersionAvailable = isNewVersion,
                CurrentVersion = _currentVersion,
                LatestVersion = latestVersion,
                DownloadUrl = release.HtmlUrl,
                ReleaseNotes = release.Body ?? "No release notes available."
            };
        }
        catch (Exception ex)
        {
            return new VersionCheckResult 
            { 
                IsSuccess = false, 
                ErrorMessage = $"Error checking for updates: {ex.Message}" 
            };
        }
    }

    private int CompareVersions(string version1, string version2)
    {
        // Simple version comparison (e.g., "2.1" vs "2.0")
        var v1Parts = version1.Split('.').Select(int.Parse).ToArray();
        var v2Parts = version2.Split('.').Select(int.Parse).ToArray();

        int maxLength = Math.Max(v1Parts.Length, v2Parts.Length);

        for (int i = 0; i < maxLength; i++)
        {
            int v1Part = i < v1Parts.Length ? v1Parts[i] : 0;
            int v2Part = i < v2Parts.Length ? v2Parts[i] : 0;

            if (v1Part > v2Part) return 1;
            if (v1Part < v2Part) return -1;
        }

        return 0; // Versions are equal
    }
}

public class VersionCheckResult
{
    public bool IsSuccess { get; set; }
    public bool IsNewVersionAvailable { get; set; }
    public string CurrentVersion { get; set; } = string.Empty;
    public string LatestVersion { get; set; } = string.Empty;
    public string DownloadUrl { get; set; } = string.Empty;
    public string ReleaseNotes { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
}

public class GitHubRelease
{
    [JsonPropertyName("tag_name")]
    public string TagName { get; set; } = string.Empty;

    [JsonPropertyName("html_url")]
    public string HtmlUrl { get; set; } = string.Empty;

    [JsonPropertyName("body")]
    public string? Body { get; set; }

    [JsonPropertyName("published_at")]
    public DateTime PublishedAt { get; set; }
}
