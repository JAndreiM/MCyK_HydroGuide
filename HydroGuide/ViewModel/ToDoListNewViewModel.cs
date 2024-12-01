using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HydroGuide.Model;
using HydroGuide.Services;
using HydroGuide.ViewDetail;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

namespace HydroGuide.ViewModel
{
    public partial class ToDoListNewViewModel(TDLDatabaseService TDLDatabaseService) : BaseViewModel
    {
        private readonly TDLDatabaseService _databaseService = TDLDatabaseService;

        public ObservableCollection<TDLObject> ToDoList { get; } = [];

        [ObservableProperty]
        string? titleInput;

        [ObservableProperty]
        DateTime selectedDateInput = DateTime.Now;

        [ObservableProperty]
        TimeSpan selectedTimeInput = DateTime.Now.TimeOfDay;

        [ObservableProperty]
        string? noteInput;

        [ObservableProperty]
        string? plantInput;

        [ObservableProperty]
        string? nutrientInput;

        [ObservableProperty]
        string? amountInput;

        [ObservableProperty]
        string? plandurationInput;

        [ObservableProperty]
        string? onceeveryInput;

        [ObservableProperty]
        ObservableCollection<string> addedPlants = [];

        [RelayCommand]
        async Task RemovePlant(string PlantToExterminate)
        {
            if (IsBusy)
                return;

            try
            {
                Debug.Write(PlantToExterminate);
                IsBusy = true;
                bool ans = await Shell.Current.DisplayAlert("REMOVE", "Are you certain to remove the selected task?", "YES", "NO");
                
                if (ans)
                    AddedPlants.Remove(PlantToExterminate);
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Error while trying to remove a task: {ex}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task AddPlantToList()
        {
            IsBusy = true;
            try
            {
                if (string.IsNullOrWhiteSpace(PlantInput))
                {
                    await Shell.Current.DisplayAlert("Error", "Inputted plant is either null or empty", "OK");
                    return;
                }

                if (string.IsNullOrWhiteSpace(NutrientInput))
                {
                    await Shell.Current.DisplayAlert("Error", "Inputted nutrient is either null or empty", "OK");
                    return;
                }

                if (string.IsNullOrWhiteSpace(AmountInput))
                {
                    await Shell.Current.DisplayAlert("Error", "Inputted amount is either null or empty", "OK");
                    return;
                }

                AddedPlants.Add($"{PlantInput} > Use {AmountInput}ml of {NutrientInput}");
                PlantInput = "";
                NutrientInput = "";
                AmountInput = "";
            }
            catch (Exception ex)
            {
                Debug.Write("Error: " + ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public string CompilePlants()
        {
            string fulltaskedplant = "";

            foreach (var taskedplant in AddedPlants)
            {
                // Append each element to fulltaskedplant with the separator
                fulltaskedplant += taskedplant + "?#@"; // Append with separator
            }

            // Optionally, remove the last separator (if you don't want it at the end)
            if (fulltaskedplant.EndsWith("?#@"))
            {
                fulltaskedplant = fulltaskedplant.Substring(0, fulltaskedplant.Length - 3); // Remove last separator
            }


            return fulltaskedplant;
        }

        [RelayCommand]
        async Task FinalizePlan()
        {
            IsBusy = true;
            try
            {

                if (string.IsNullOrWhiteSpace(TitleInput))
                {
                    await Shell.Current.DisplayAlert("Warning", "No task title has been inputed", "OK");
                    return;
                }

                if (string.IsNullOrWhiteSpace(PlandurationInput))
                {
                    await Shell.Current.DisplayAlert("Warning", "No task plan duration has been set", "OK");
                    return;
                }

                if (string.IsNullOrWhiteSpace(OnceeveryInput))
                {
                    await Shell.Current.DisplayAlert("Warning", "No once every when has been set", "OK");
                    return;
                }

                if (AddedPlants.Count <= 0)
                {
                    await Shell.Current.DisplayAlert("Warning", "No tasked plant found", "OK");
                    return;
                }

                if (string.IsNullOrEmpty(NoteInput))
                {
                    bool ans = await Shell.Current.DisplayAlert("Warning", "You have not left any note. Would you like to continue?", "YES", "NO");
                    if (!ans)
                        return;
                }

                string fulltaskedplant = CompilePlants();

                if (fulltaskedplant.Length <= 0)
                {
                    await Shell.Current.DisplayAlert("Warning", "No tasked plant found", "OK");
                    return;
                }

                Debug.WriteLine(fulltaskedplant);

                bool success = DateTime.TryParse(SelectedDateInput.ToString(), out DateTime RealDealDate);

                if (success)
                {
                    // Now RealDealDate has the successfully parsed DateTime value
                    Debug.WriteLine(RealDealDate);
                }
                else
                {
                    // Parsing failed, handle the error accordingly
                    Debug.WriteLine("Invalid date format");
                }

                success = TimeSpan.TryParse(SelectedTimeInput.ToString(), out TimeSpan RealDealTime);

                if (success)
                {
                    // Now RealDealDate has the successfully parsed DateTime value
                    Debug.WriteLine(RealDealTime);
                }
                else
                {
                    // Parsing failed, handle the error accordingly
                    Debug.WriteLine("Invalid Time format");
                }

                // Use a base DateTime to add the TimeSpan
                DateTime dateTime = DateTime.Today.Add(RealDealTime);

                // Format as AM/PM
                string formattedTime = dateTime.ToString("hh:mm tt");  // Output: 02:30 PM

                var taskObject = new TDLObject
                {
                    Title = TitleInput,
                    Notes = NoteInput ?? "N/A",
                    Plants = fulltaskedplant,
                    Date = RealDealDate.ToString("yyyy/MM/dd - dddd"),
                    Time = formattedTime,
                    Accomplished = false,
                    OnceEvery = int.Parse(OnceeveryInput ?? "1"),
                    DayDuration = int.Parse(PlandurationInput ?? "1"),
                    AccomplishedDay = ""
                };

                await _databaseService.AddRecord(taskObject);
                await Shell.Current.DisplayAlert("Success", "Plan has been finalized and saved!", "OK");
                await Shell.Current.GoToAsync("..");


            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while finalizing plan: {ex.Message}"); // Log any exceptions
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}