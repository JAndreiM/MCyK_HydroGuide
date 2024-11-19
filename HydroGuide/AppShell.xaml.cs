using HydroGuide.ViewDetail;

namespace HydroGuide
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ManualDetail),typeof(ManualDetail));

        }


    }
}
