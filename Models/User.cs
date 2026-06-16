using System.ComponentModel.DataAnnotations;

namespace TravelLoggerGroup.Models;

public class User
{
    public int Id { get; set; } 
    [Required]
    public string Email { get; set; } 
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public List<Log> Logs { get; set; }
    public List<Recommendation> Recommendations { get; set; }
}