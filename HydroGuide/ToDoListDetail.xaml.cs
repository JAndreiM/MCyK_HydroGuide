using HydroGuide.Model;
using HydroGuide.ViewModel;

namespace HydroGuide;

public partial class ToDoListDetail : ContentPage
{
	public ToDoListDetail(ToDoListDetailViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}