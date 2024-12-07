using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HydroGuide.Model;
using HydroGuide.Services;
using HydroGuide.ViewDetail;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Linq;

namespace HydroGuide.ViewModel;

public partial class ConvertViewModel(BigMama BigMamaService) : BaseViewModel
{
    public ObservableCollection<ManualObject> ManualList { get; } = [];
    private readonly BigMama BigMamaService = BigMamaService;


    [RelayCommand]
    async Task GetManualListAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            var manuallist = await BigMamaService.GetManualList();

            if (ManualList.Count != 0)
                ManualList.Clear();

            if (manuallist != null)
            {
                foreach (ManualObject ManualInfo in manuallist)
                {
                    //await Task.Delay(400);
                    ManualList.Add(ManualInfo);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get monkeys: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            RenewNutrientOption();
            IsBusy = false;
        }

    }

    [ObservableProperty]
    public ObservableCollection<string> nutrientOptionList = [];

    [RelayCommand]
    public void RenewNutrientOption()
    {
        NutrientOptionList.Clear();
        foreach (var obj in ManualList)
        {
            //Debug.WriteLine($"{obj.NutrientMLtoLiter} :: {obj.Name}");
            if (obj.Category == "NUTRIENT" && obj.NutrientMLtoLiter != null)
            {
                NutrientOptionList.Add(obj.Name);
                SelectedNutrient = obj.Name;
            }
        }
    }

    [ObservableProperty]
    string? selectedNutrient;

    [ObservableProperty]
    string inputtedNutrientAmount = "";

    [ObservableProperty]
    string inputtedWaterAmount = "";

    [ObservableProperty]
    string selectedInsert = "nutrientHolder";

    [RelayCommand]
    public void NewInsertSelect(string s)
    {
        SelectedInsert = s;
    }

    [RelayCommand]
    public void KeyTapped(string s)
    {
        Debug.WriteLine($"{s} :: {SelectedInsert}");
       if(SelectedInsert == "nutrientHolder")
        {
            if (s == "1")
                InputtedNutrientAmount += s;
            if (s == "2")
                InputtedNutrientAmount += s;
            if (s == "3")
                InputtedNutrientAmount += s;
            if (s == "4")
                InputtedNutrientAmount += s;
            if (s == "5")
                InputtedNutrientAmount += s;
            if (s == "6")
                InputtedNutrientAmount += s;
            if (s == "7")
                InputtedNutrientAmount += s;
            if (s == "8")
                InputtedNutrientAmount += s;
            if (s == "9")
                InputtedNutrientAmount += s;
            if (s == "0")
                InputtedNutrientAmount += s;
            if (s == "C")
                InputtedNutrientAmount = "";
            if (s == "<-")
                InputtedNutrientAmount = InputtedNutrientAmount.Substring(0, InputtedNutrientAmount.Length - 1);
        }
        else
        {
            if (s == "1")
                InputtedWaterAmount += s;
            if (s == "2")
                InputtedWaterAmount += s;
            if (s == "3")
                InputtedWaterAmount += s;
            if (s == "4")
                InputtedWaterAmount += s;
            if (s == "5")
                InputtedWaterAmount += s;
            if (s == "6")
                InputtedWaterAmount += s;
            if (s == "7")
                InputtedWaterAmount += s;
            if (s == "8")
                InputtedWaterAmount += s;
            if (s == "9")
                InputtedWaterAmount += s;
            if (s == "0")
                InputtedWaterAmount += s;
            if (s == "C")
                InputtedWaterAmount = "";
            if (s == "<-")
                InputtedWaterAmount = InputtedWaterAmount.Substring(0, InputtedWaterAmount.Length - 1);
        }
    }

    partial void OnInputtedNutrientAmountChanged(string? oldValue, string newValue)
    {
        try
        {
            
            if (newValue == "")
            {
                InputtedWaterAmount = "";
            }
            else
            {
                var sobj = ManualList.FirstOrDefault(obj => string.Equals(obj.Name, SelectedNutrient, StringComparison.OrdinalIgnoreCase));
                if (sobj != null)
                {
                    InputtedWaterAmount = (double.Parse(InputtedNutrientAmount) * sobj.NutrientMLtoLiter).ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error: {ex}");
        }
    }

    partial void OnInputtedWaterAmountChanged(string? oldValue, string newValue)
    {
        try
        {
            if(newValue == "")
            {
                InputtedNutrientAmount = "";
            }
            else
            {
                var sobj = ManualList.FirstOrDefault(obj => string.Equals(obj.Name, SelectedNutrient, StringComparison.OrdinalIgnoreCase));
                if (sobj != null)
                {
                    InputtedNutrientAmount = (double.Parse(InputtedWaterAmount) / sobj.NutrientMLtoLiter).ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error: {ex}");
        }
    }

    partial void OnSelectedNutrientChanged(string? oldValue, string newValue)
    {
        SelectedInsert = "nutrientHolder";
        InputtedNutrientAmount = "";
        InputtedWaterAmount = "";
    }
}