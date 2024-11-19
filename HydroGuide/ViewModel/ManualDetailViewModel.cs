using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HydroGuide.Model;
using System.Diagnostics;

namespace HydroGuide.ViewModel;

[QueryProperty("ManualImage", "ManualImage")]
[QueryProperty("ManualName", "ManualName")]
[QueryProperty("ManualDetail", "ManualDetail")]
[QueryProperty("ManualCategory", "ManualCategory")]
public partial class ManualDetailViewModel : ObservableObject
{
    [ObservableProperty]
    string? manualImage;

    [ObservableProperty]
    string? manualName;

    [ObservableProperty]
    string? manualDetail;

    [ObservableProperty]
    string? manualCategory;
}
