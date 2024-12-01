using HydroGuide.Model;
using HydroGuide.ViewModel;

namespace HydroGuide;

public partial class ToDoListDetail : ContentPage
{
    private readonly ToDoListDetailViewModel _todolistDetailViewModel;
    public ToDoListDetail(ToDoListDetailViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		_todolistDetailViewModel = vm;
	}

	public void Checkboxpressed(object sender, EventArgs e)
	{
		_todolistDetailViewModel.CheckBoxClickedCommand.Execute(null);
	}
}