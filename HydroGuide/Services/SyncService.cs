using HydroGuide.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydroGuide.Services;

public class SyncService
{
    private readonly ManualDatabaseService _localDatabaseService;

    public SyncService(ManualDatabaseService localDatabaseService)
    {
        _localDatabaseService = localDatabaseService;
    }

    public async Task SyncDataWithOnlineAsync(List<ManualObject> onlineData)
    {
        // Fetch all local data
        var localData = await _localDatabaseService.GetAllRecordsAsync();

        // Convert both lists to dictionaries for easy comparison
        var onlineDataDict = onlineData.ToDictionary(x => x.Name);
        var localDataDict = localData.ToDictionary(x => x.Name);

        // Handle additions and updates
        foreach (var onlineItem in onlineData)
        {
            if (!localDataDict.TryGetValue(onlineItem.Name, out var localItem))
            {
                // Item is online but not locally; add it
                await _localDatabaseService.AddRecord(onlineItem);
            }
            else if (!SyncService.AreObjectsEqual(onlineItem, localItem))
            {
                // Item exists but is different; update it
                await _localDatabaseService.UpdateRecord(onlineItem);
            }
        }

        // Handle deletions
        foreach (var localItem in localData)
        {
            if (!onlineDataDict.ContainsKey(localItem.Name))
            {
                // Item is local but not online; delete it
                await _localDatabaseService.DeleteRecord(localItem.Id);

                // Delete associated image
                SyncService.DeleteLocalImage(localItem.Image);
            }
        }
    }

    private static bool AreObjectsEqual(ManualObject onlineItem, ManualObject localItem)
    {
        // Compare all relevant properties
        return onlineItem.Name == localItem.Name &&
               onlineItem.Details == localItem.Details &&
               onlineItem.Category == localItem.Category &&
               onlineItem.Image == localItem.Image;
    }

    private static void DeleteLocalImage(string imagePath)
    {
        if (File.Exists(imagePath))
        {
            File.Delete(imagePath);
            Console.WriteLine($"Deleted local image: {imagePath}");
        }
    }
}
