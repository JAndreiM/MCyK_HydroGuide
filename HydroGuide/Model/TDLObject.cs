using SQLite;

namespace HydroGuide.Model;

public class TDLObject
{
    [PrimaryKey, AutoIncrement]
    public int? Id { get; set; }

    public string Title { get; set; } = "N/A";
    public string Date { get; set; } = "N/A";
    public string Time { get; set; } = "N/A";
    public string Plants { get; set; } = "N/A";
    public string Notes { get; set; } = "N/A";

    public Boolean Accomplished { get; set; } = false;

    public int OnceEvery { get; set; } = 1;
    public int DayDuration { get; set; } = 1;
    public string AccomplishedDay { get; set; } = "";
}