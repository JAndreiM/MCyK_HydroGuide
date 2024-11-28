using HydroGuide.Model;
using HydroGuide.ViewModel;
using System.Collections.ObjectModel;

namespace HydroGuide;

public partial class ToDoListPage : ContentPage
{
    private readonly ToDoListViewModel _todolistViewModel;
    public ObservableCollection<TDLObject> ToDoList { get; } = [];
    public ToDoListPage(ToDoListViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		_todolistViewModel = vm;

        //ToDoList.Add(new TDLObject { Title = "Task 1", Date = DateTime.Today.ToString("yyyy/MM/dd - dddd"), Time = DateTime.Now.ToString("HH:mm tt"), Plants = "Plant A > Use # ml of nutrient A?#@Plant B > Use # ml of nutrient B?#@Plant E > Use # ml of nutrient A", Notes = "Can be found in sector C of the Garden.", Accomplished = true });
        //ppList.ItemsSource = ToDoList;
    
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = _todolistViewModel.GetToDoListCommand.ExecuteAsync(null);  // Execute the command
        OnPropertyChanged(nameof(ToDoList));
    }
}