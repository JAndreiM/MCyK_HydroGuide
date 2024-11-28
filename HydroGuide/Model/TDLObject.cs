using SQLite;

namespace HydroGuide.Model;

public class TDLObject
{
    [PrimaryKey, AutoIncrement]
    public int? Id { get; set; }

    public string? Title { get; set; }
    public string? Date { get; set; }
    public string? Time { get; set; }
    public string? Plants { get; set; }
    public string? Notes { get; set; }
    public Boolean Accomplished { get; set; }
}