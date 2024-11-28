using CommunityToolkit.Mvvm.ComponentModel;
using HydroGuide.Model;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace HydroGuide.ViewModel;

[QueryProperty("TdlTitle", "TdlTitle")]
[QueryProperty("TdlDate", "TdlDate")]
[QueryProperty("TdlTime", "TdlTime")]
[QueryProperty("TdlPlants", "TdlPlants")]
[QueryProperty("TdlNotes", "TdlNotes")]
[QueryProperty("TdlAccomplished", "TdlAccomplished")]
public partial class ToDoListDetailViewModel : BaseViewModel
{
    [ObservableProperty]
    string? tdlTitle;

    [ObservableProperty]
    string? tdlDate;

    [ObservableProperty]
    string? tdlTime;

    [ObservableProperty]
    string? tdlPlants;

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
}
