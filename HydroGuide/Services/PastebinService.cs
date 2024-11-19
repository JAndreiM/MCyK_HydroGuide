using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HydroGuide.Services;

public class PastebinService
{
    private readonly HttpClient _httpClient;

    public PastebinService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<string?> GetStringFromPastebinAsync(string url) // Note the nullable return type
    {
        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Will throw if not successful

            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException httpEx)
        {
            Console.WriteLine($"Request error: {httpEx.Message}");
            return null; // Return null on error
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
            return null; // Return null on unexpected errors
        }
    }
}
