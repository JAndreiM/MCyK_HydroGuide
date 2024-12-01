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
            #if DEBUG
                        builder.Logging.AddDebug();
            #else
                        builder.ConfigureMauiHandlers(handlers => { });
            #endif


            //Register all page and vm
            builder.Services.AddSingleton<BigMama>();
            builder.Services.AddSingleton<SyncService>();

            builder.Services.AddSingleton<AboutPage>();

            builder.Services.AddSingleton<MainPage>();

            builder.Services.AddSingleton<ManualPage>();
            builder.Services.AddSingleton<ManualViewModel>();
            builder.Services.AddSingleton<ManualDatabaseService>();

            builder.Services.AddTransient<ManualDetail>();
            builder.Services.AddTransient<ManualDetailViewModel>();

            builder.Services.AddSingleton<ToDoListPage>();
            builder.Services.AddSingleton<ToDoListViewModel>();
            builder.Services.AddSingleton<TDLDatabaseService>();

            builder.Services.AddTransient<ToDoListDetail>();
            builder.Services.AddTransient<ToDoListDetailViewModel>();

            builder.Services.AddTransient<ToDoListNew>();
            builder.Services.AddTransient<ToDoListNewViewModel>();


            builder.Services.AddSingleton<ConvertPage>();
//#endif



            return builder.Build();
        }
    }
}
