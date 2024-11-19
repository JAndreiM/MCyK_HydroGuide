using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HydroGuide.Model;
using HydroGuide.Services;
using HydroGuide.ViewDetail;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

namespace HydroGuide.ViewModel;

public partial class ManualViewModel(BigMama BigMamaService) : BaseViewModel 
{
    public ObservableCollection<ManualObject> ManualList { get; } = [];
    private readonly BigMama BigMamaService = BigMamaService;

    [RelayCommand]
    async Task Tap(ManualObject s)
    {
        try
        {
           await Shell.Current.GoToAsync($"{nameof(ManualDetail)}",
           new Dictionary<string, object>
           {
                { "ManualImage", s.Image},
                { "ManualName", s.Name},
                { "ManualDetail", s.Details},
                { "ManualCategory", s.Category}
           });
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to navigate detail: {ex.Message}"); // Log any exceptions
        }
    }

    [RelayCommand]
    async Task GetManualListAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            var manuallist = await BigMamaService.GetManualList();

            if (ManualList.Count != 0)
                ManualList.Clear();

            if (manuallist != null)
            {
                foreach (ManualObject ManualInfo in manuallist)
                {
                    //await Task.Delay(400);
                    ManualList.Add(ManualInfo);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get monkeys: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }

    }
}