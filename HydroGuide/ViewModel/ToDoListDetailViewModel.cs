using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HydroGuide.Model;
using HydroGuide.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace HydroGuide.ViewModel;

[QueryProperty("TdlTitle", "TdlTitle")]
[QueryProperty("TdlDate", "TdlDate")]
[QueryProperty("TdlTime", "TdlTime")]
[QueryProperty("TdlPlants", "TdlPlants")]
[QueryProperty("TdlNotes", "TdlNotes")]
[QueryProperty("TdlAccomplished", "TdlAccomplished")]
[QueryProperty("TdlOnceEvery", "TdlOnceEvery")]
[QueryProperty("TdlDayDuration", "TdlDayDuration")]
[QueryProperty("TdlDayCheck", "TdlDayCheck")]
public partial class ToDoListDetailViewModel(TDLDatabaseService TDLDatabaseService) : BaseViewModel
{
    private readonly TDLDatabaseService _databaseService = TDLDatabaseService;



    [ObservableProperty]
    string? tdlTitle;

    [ObservableProperty]
    string tdlDate = DateTime.Now.ToString();

    [ObservableProperty]
    string? tdlTime;

    [ObservableProperty]
    string? tdlPlants;

    [ObservableProperty]
    string? tdlOnceEvery;

    // Change the type to List<string> for flexibility
    [ObservableProperty]
    ObservableCollection<string> plantslist = [];

    // Partial method that gets called when the property changes
    partial void OnTdlPlantsChanged(string? oldValue, string? newValue)
    {
        // Split the string by commas and update the plantslist
        if (!string.IsNullOrEmpty(newValue))
        {
            // Convert the result of Split into a List<string>
            Plantslist = [..newValue.Split("?#@")];
        }
    }

    [ObservableProperty]
    string? tdlNotes;

    [ObservableProperty]
    private string? tdlAccomplished;

    [ObservableProperty]
    public Boolean isAccomplished;

    partial void OnTdlAccomplishedChanged(string? oldValue, string? newValue)
    {
        if (bool.TryParse(newValue, out var result))
        {
            IsAccomplished = result;
        }
    }


    [ObservableProperty]
    private string? tdlDayDuration;

    [ObservableProperty]
    public int currentDayDuration = 1;

    partial void OnTdlDayDurationChanged(string? oldValue, string? newValue)
    {
        if (int.TryParse(newValue, out var result))
        {
            CurrentDayDuration = result;
        }
        else
        {
            CurrentDayDuration = 1;
        }
    }

    [ObservableProperty]
    string? tdlDayCheck;

    partial void OnTdlDayCheckChanged(string? oldValue, string? newValue)
    {
        Debug.WriteLine($"TdlDayCheck received: {newValue}");

        if (string.IsNullOrEmpty(TdlDayCheck))
            return;

        var accomplishedDays = TdlDayCheck.Split(',');
        for (int i = 0; i < DayTallyList.Count; i++)
        {
            if (accomplishedDays.Contains(i.ToString()))
            {
                DayTallyList[i].IsAccomplished = true;
            }
            else
            {
                DayTallyList[i].IsAccomplished = false;
            }

        }

    }

    public ObservableCollection<DayTallyObject> DayTallyList { get; } = [];

    partial void OnCurrentDayDurationChanged(int oldValue, int newValue)
    {
        try
        {
            DayTallyList.Clear();
            var cDate = DateTime.Parse(TdlDate.Split(" -")[0]);
            for (int i = 1; i <= newValue; i +=int.Parse(TdlOnceEvery) )
            {
                Debug.WriteLine($"{cDate.AddDays(i - 1).ToString("MMMM dd, yyyy - dddd")}(Day {i})");
                //DayTallyList.Add(cDate.ToString("MMMM dd, yyyy - dddd") + $"(Day {i})");
                DayTallyList.Add(new DayTallyObject { CDate = $"{cDate.AddDays(i - 1).ToString("MMMM dd, yyyy - dddd")}(Day {i})", IsAccomplished = false });
            }
        }
        catch(Exception ex)
        {
            Debug.WriteLine($"Error encountered on 'OnCurrentDayDurationChanged': {ex}");
        }
    }

    [RelayCommand]
    public void CheckBoxClicked()
    {
        string z = "";
        int count = 0;
        for (int i = 0; i < CurrentDayDuration; i += int.Parse(TdlOnceEvery))
        {
            count++;
        }

        for (int i = 0; i < count; i++)
        {
            if (DayTallyList[i].IsAccomplished == true)
            {
                if (z == "")
                {
                    z = i.ToString();
                }
                else
                {
                    z = z + "," + i.ToString();
                }
            }
        }

        if (string.IsNullOrEmpty(z))
            return;

        _databaseService.UpdateAccomplishedDay(TdlTitle ?? "", z);
        /*
          int.TryParse(TdlOnceEvery, out int parsedOnceEvery);
        int.TryParse(TdlDayDuration, out int parsedDayDuration);

        var taskObject = new TDLObject
        {
            Title = TdlTitle ?? "N/A",
            Notes = TdlNotes ?? "N/A",
            Plants = TdlPlants ?? "N/A",
            Date = TdlDate,
            Time = TdlTime ?? "N/A",
            Accomplished = false,
            OnceEvery = parsedOnceEvery, // Parsed value
            DayDuration = parsedDayDuration, // Parsed value
            AccomplishedDay = z // Ensure `z` is an int
        };*/
    }
}
