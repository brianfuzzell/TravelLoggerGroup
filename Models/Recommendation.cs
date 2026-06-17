using System.ComponentModel.DataAnnotations;

namespace TravelLoggerGroup.Models;

public class Recommendation
{
    public int Id { get; set; } 
    public int CityId { get; set; } 
    public int UserId { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    public User User { get; set; } 
    public List<UpVote> UpVotes { get; set; } 
}