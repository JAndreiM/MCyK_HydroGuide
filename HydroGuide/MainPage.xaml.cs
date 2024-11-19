using CommunityToolkit.Maui.Core.Platform;
using System.Diagnostics;

namespace HydroGuide
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();

        }
        public async void TodolistClicked(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync("///" + nameof(ToDoListPage), animate: false);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to navigate: {ex.Message}"); // Log any exceptions
            }
        }

        public async void ManualClicked(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync("///" + nameof(ManualPage), animate: false);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to navigate: {ex.Message}"); // Log any exceptions
            }
        }

        public async void ConvertClicked(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync("///" + nameof(ConvertPage), animate: false);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to navigate: {ex.Message}"); // Log any exceptions
            }
        }

        public async void FollowClicked(object sender, EventArgs e)
        {
            var url = "https://www.facebook.com/@BulacanHydroponicsSupply"; // Make sure to include http:// or https://
            try
            {
                await Launcher.OpenAsync(url); // Use Launcher to open the URL
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to open URL: {ex.Message}"); // Log any exceptions
            }
        }
    }

}
