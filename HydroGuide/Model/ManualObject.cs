using System.Text.Json.Serialization;

namespace HydroGuide.Model;

public class ManualObject
{
    public required string Image { get; set; }
    public required string Name { get; set; }
    public required string Details { get; set; }
    public required string Category { get; set; }
}