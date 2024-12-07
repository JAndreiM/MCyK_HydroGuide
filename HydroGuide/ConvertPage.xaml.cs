using HydroGuide.ViewModel;

namespace HydroGuide;

public partial class ConvertPage : ContentPage
{
	private readonly ConvertViewModel _ConvertViewModel;
	public ConvertPage(ConvertViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		_ConvertViewModel = vm;

		_ConvertViewModel.GetManualListCommand.Execute(null);
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();

		_ConvertViewModel.RenewNutrientOptionCommand.Execute(null);
	}
}