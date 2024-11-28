using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HydroGuide.Model;
using HydroGuide.Services;
using HydroGuide.ViewDetail;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

namespace HydroGuide.ViewModel;

public partial class ToDoListViewModel(BigMama BigMamaService, TDLDatabaseService tDLDatabaseService) : BaseViewModel 
{

    public ObservableCollection<TDLObject> ToDoList { get; } = [];
    private readonly BigMama BigMamaService = BigMamaService;
    private readonly TDLDatabaseService _TDLDatabase = tDLDatabaseService;

    [RelayCommand]
    async Task Tap(TDLObject s)
    {
        try
        {
            Debug.WriteLine(s.Plants);

           await Shell.Current.GoToAsync($"{nameof(ToDoListDetail)}",
           new Dictionary<string, object>
           {
                { "TdlTitle", s.Title ?? "N/A"},
                { "TdlDate", s.Date ?? "N/A"},
                { "TdlTime", s.Time ?? "N/A"},
                { "TdlPlants", s.Plants ?? "N/A"},
                { "TdlNotes", s.Notes ?? "N/A"},
                { "TdlAccomplished", s.Accomplished.ToString()}
           });
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to navigate detail: {ex.Message}"); // Log any exceptions
        }
    }

    [RelayCommand]
    async Task TapForNew()
    {
        try
        {
            await Shell.Current.GoToAsync($"{nameof(ToDoListNew)}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to navigate detail: {ex.Message}"); // Log any exceptions
        }
    }

    [RelayCommand]
    async Task GetToDoListAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            var todolist = await BigMamaService.GetToDoList();

            if (ToDoList.Count != 0)
                ToDoList.Clear();

            if (todolist != null)
            {
                foreach (TDLObject TDLInfo in todolist)
                {
                    //await Task.Delay(400);
                    ToDoList.Add(TDLInfo);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get to-do lists: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;

        }
    }

    [RelayCommand]
    async Task DeleteElement(int id)
    {
        IsBusy = true;
        try
        {
            await _TDLDatabase.DeleteRecord(id);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to delete element: {ex.Message}"); // Log any exceptions
        }
        finally
        {
            IsBusy = false;
            GetToDoListCommand.Execute(null);
        }
    }
}