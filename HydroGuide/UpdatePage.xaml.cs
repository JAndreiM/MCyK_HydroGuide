using HydroGuide.Services;

namespace HydroGuide;

public partial class UpdatePage : ContentPage
{
    //private readonly PastebinService? _pastebinService;

    public UpdatePage()
	{
        InitializeComponent();
	}

    private async void OnUpdateTapped(object sender, EventArgs e)
    {
        // Handle the tap event here
        bool answer = await DisplayAlert("Data Update", "Data v0.2 has been found! would you like to update?", "Yes", "No");
        if (answer)
        {
        //    string pastebinUrl = "https://pastebin.com/raw/8HQ2gepr"; // Replace with your desired URL
        //    string? data = await _pastebinService?.GetStringFromPastebinAsync(pastebinUrl); // Use null conditional operator

        //    if (data != null)
        //    {
        //        await DisplayAlert("Response", data, "OK");
        //    }
        //    else
        //    {
        //        await DisplayAlert("Response", "Null dawg", "OK");
        //    }
        //}
        //else
        //{
        //    await DisplayAlert("Response", "Did not do anythin'", "OK");
        }
    }
}