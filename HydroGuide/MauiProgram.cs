using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using HydroGuide.ViewModel;
using HydroGuide.ViewDetail;
using HydroGuide.Services;

namespace HydroGuide
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

//#if DEBUG
            builder.Logging.AddDebug();

            //Register all page and vm
            builder.Services.AddSingleton<BigMama>();

            builder.Services.AddSingleton<AboutPage>();

            builder.Services.AddSingleton<MainPage>();

            builder.Services.AddSingleton<ManualPage>();
            builder.Services.AddSingleton<ManualViewModel>();

            builder.Services.AddTransient<ManualDetail>();
            builder.Services.AddTransient<ManualDetailViewModel>();

            builder.Services.AddSingleton<ToDoListPage>();

            builder.Services.AddSingleton<ConvertPage>();
//#endif

            return builder.Build();
        }
    }
}
