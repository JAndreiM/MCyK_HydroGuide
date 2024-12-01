using SQLite;
using System.Text.Json.Serialization;

namespace HydroGuide.Model;

public class ManualObject
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public  string Image { get; set; } = "";
    public  string Name { get; set; } = "N/A";
    public  string Details { get; set; } = "N/A";
    public  string Category { get; set; } = "N/A";
}