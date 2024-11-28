using HydroGuide.ViewModel;

namespace HydroGuide;

public partial class ToDoListNew : ContentPage
{
	public ToDoListNew(ToDoListNewViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;

        DatePickerElement.MinimumDate = DateTime.Now;
        TimePickerElement.Time = DateTime.Now.TimeOfDay;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        DatePickerElement.MinimumDate = DateTime.Now;
        TimePickerElement.Time = DateTime.Now.TimeOfDay;
    }
}