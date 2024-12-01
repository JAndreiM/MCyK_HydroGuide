using HydroGuide.Model;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace HydroGuide.Services;

public class BigMama
{
    readonly HttpClient httpClient;
    private readonly TDLDatabaseService _TDLdatabaseService;
    private readonly ManualDatabaseService _ManualdatabaseService;
    private readonly SyncService _SyncDatabaseService;
    public BigMama(TDLDatabaseService TDLDatabaseService, ManualDatabaseService ManualDatabaseService, SyncService syncService)
    {
        httpClient = new HttpClient();
        _TDLdatabaseService = TDLDatabaseService;
        _ManualdatabaseService = ManualDatabaseService;
        _SyncDatabaseService = syncService;
    }

    List<ManualObject>? ManualList;
    public async Task<List<ManualObject>?> GetManualList()
    {
        ManualList = [];
        /*
        if (ManualList?.Count > 0)
            return ManualList;

        Online
       var response = await httpClient.GetAsync("https://www.montemagno.com/monkeys.json");
        if (response.IsSuccessStatusCode)
        {
            ManualList = await response.Content.ReadFromJsonAsync<List<ManualObject>>();
        }


        var REDresponse = await httpClient.GetAsync("https://pastebin.com/raw/T09aDuNm");
        if (REDresponse.IsSuccessStatusCode)
        {
            var response = await httpClient.GetAsync(REDresponse.Content.ToString());
            if (response.IsSuccessStatusCode)
            {
                ManualList = await response.Content.ReadFromJsonAsync<List<ManualObject>>();
                return ManualList;
            }
        } */
        try
        {
            // Attempt to fetch data from the online source
            var response = await httpClient.GetAsync("https://pastebin.com/raw/qQxaqtps");

            if (response.IsSuccessStatusCode)
            {
                var MList = await response.Content.ReadFromJsonAsync<List<ManualObject>>();

                // Sync online data with local database
                if (MList != null)
                    await _SyncDatabaseService.SyncDataWithOnlineAsync(MList);
            }
            else
            {
                // Display alert if no internet or server issues
                await Shell.Current.DisplayAlert("No Internet", "Using local database to check manual list", "OK");
            }
        }
        catch (HttpRequestException ex)
        {
            // Handle cases where there is no internet connection or other request failures
            Debug.WriteLine($"Network error: {ex.Message}");
            await Shell.Current.DisplayAlert("No Internet", "Using local database to check manual list", "OK");
        }
        catch (Exception ex)
        {
            // Handle unexpected exceptions
            Debug.WriteLine($"Unexpected error: {ex.Message}");
            await Shell.Current.DisplayAlert("Error", "An unexpected error occurred. Using local database.", "OK");
        }


        ManualList = await _ManualdatabaseService.GetAllRecordsAsync();

        return ManualList;
    }

    List<TDLObject>? ToDoList;
    public async Task<List<TDLObject>?> GetToDoList()
    {
        ToDoList = [];
        try
        {
            

            ToDoList = await _TDLdatabaseService.GetAllRecordsAsync();

            return ToDoList;
        }catch(Exception ex)
        {
            Debug.WriteLine($"Error: {ex}");
            return ToDoList;
        }
    }
}
