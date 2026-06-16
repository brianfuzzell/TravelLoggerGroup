using System.ComponentModel.DataAnnotations;

namespace TravelLoggerGroup.Models;

public class City
{
    public int Id { get; set; } 
    [Required]
    public string Name { get; set; } 
    public List<Log> Logs { get; set; } 
    public List<Recommendation> Recommendations { get; set; }
}