using CommunityToolkit.Mvvm.ComponentModel;
using HydroGuide.Model;
using HydroGuide.ViewDetail;
using HydroGuide.ViewModel;
using System.Collections.ObjectModel;

namespace HydroGuide;

public partial class ManualPage : ContentPage
{
    private readonly ManualViewModel _manualViewModel;
    public ManualPage(ManualViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
        _manualViewModel = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await DelayFunction();  // Await the delay method
        _ = _manualViewModel.GetManualListCommand.ExecuteAsync(null);  // Execute the command
    }

    public async Task DelayFunction()
    {
        // Delay for 1 seconds (3000 milliseconds)
        await Task.Delay(1000);
        Console.WriteLine("Function delayed by 3 seconds");
    }
}