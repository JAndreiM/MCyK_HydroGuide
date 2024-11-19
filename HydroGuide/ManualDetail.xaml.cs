using HydroGuide.Model;
using HydroGuide.ViewModel;

namespace HydroGuide.ViewDetail;

public partial class ManualDetail : ContentPage
{
	public ManualDetail(ManualDetailViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}