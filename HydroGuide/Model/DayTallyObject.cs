using System.Text.Json.Serialization;

namespace HydroGuide.Model;

public class DayTallyObject
{
    public required string CDate { get; set; }
    public required bool IsAccomplished { get; set; }
}