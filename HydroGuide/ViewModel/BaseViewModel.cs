using CommunityToolkit.Mvvm.ComponentModel;

namespace HydroGuide.ViewModel;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    public bool isBusy;

    //[ObservableProperty]
    //string title;

    public bool IsNotBusy => !IsBusy;
}

