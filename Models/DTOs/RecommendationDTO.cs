using System.ComponentModel.DataAnnotations;

namespace TravelLoggerGroup.Models.DTOs;

public class RecommendationDTO
{
    public int Id { get; set; } 
    public int CityId { get; set; } 
    public int UserId { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    public UserDTO User { get; set; }
    public List<UpVoteDTO> UpVotes { get; set; } 
    public int UpVoteCount
    {
        get
        {
            return UpVotes?.Count ?? 0;
        }
    }
}