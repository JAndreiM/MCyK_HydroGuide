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
    public BigMama(TDLDatabaseService TDLDatabaseService)
    {
        httpClient = new HttpClient();
        _TDLdatabaseService = TDLDatabaseService;
    }

    List<ManualObject>? ManualList;
    public async Task<List<ManualObject>?> GetManualList()
    {
        ManualList = [];
        //if (ManualList?.Count > 0)
        //    return ManualList;

        // Online
        //var response = await httpClient.GetAsync("https://www.montemagno.com/monkeys.json");
        //if (response.IsSuccessStatusCode)
        //{
        //    ManualList = await response.Content.ReadFromJsonAsync<List<ManualObject>>();
        //}


        //var REDresponse = await httpClient.GetAsync("https://pastebin.com/raw/T09aDuNm");
        // if (REDresponse.IsSuccessStatusCode)
        // {
        //     var response = await httpClient.GetAsync(REDresponse.Content.ToString());
        //     if (response.IsSuccessStatusCode)
        //     {
        //         ManualList = await response.Content.ReadFromJsonAsync<List<ManualObject>>();
        //         return ManualList;
        //     }
        // }
        var response = await httpClient.GetAsync("https://pastebin.com/raw/qQxaqtps");
        if (response.IsSuccessStatusCode)
        {
            ManualList = await response.Content.ReadFromJsonAsync<List<ManualObject>>();
            return ManualList;
        }


        using var stream = await FileSystem.OpenAppPackageFileAsync("manualobjectdata.json");
        using var reader = new StreamReader(stream);
        var contents = await reader.ReadToEndAsync();
        ManualList = JsonSerializer.Deserialize<List<ManualObject>>(json: contents);
        if (ManualList != null)
            return ManualList;

        return ManualList;
    }

    List<TDLObject>? ToDoList;
    public async Task<List<TDLObject>?> GetToDoList()
    {
        ToDoList = [];

        ToDoList = await _TDLdatabaseService.GetAllRecordsAsync();

        return ToDoList;
    }
}
